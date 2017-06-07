using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Ersin.RestClient
{
    /// <summary> 
    /// Class to make requests
    /// </summary> 
    public class RestRequest<T>
    {

        private IRestClient _client;
        private string _path;
        private Method _method;

        /// <summary> 
        /// Default constructor sets the RestClient property for requests made by this client instance 
        /// </summary> 
        /// <param name="client">RestClient</param> 
        /// <param name="path">Path to rest service</param> 
        /// <param name="method">Http method</param> 
        public RestRequest(IRestClient client, string path, Method method)
        {

            if (client == null)
                throw new ArgumentNullException("client");

            if (String.IsNullOrEmpty(path))
                throw new ArgumentNullException("path");

            _client = client;
            _path = path;
            _method = method;
        }

        /// <summary> 
        /// Constructor for Http GET requests
        /// </summary> 
        /// <param name="client">RestClient</param> 
        /// <param name="path">Path to rest service</param> 
        public RestRequest(IRestClient client, string path)
        {

            if (client == null)
                throw new ArgumentNullException("client");

            if (String.IsNullOrEmpty(path))
                throw new ArgumentNullException("path");

            _client = client;
            _path = path;
            _method = Method.GET;
        }

        /// <summary> 
        /// Execute request
        /// </summary> 
        /// <returns>RestResponse</returns> 
        public async Task<RestResponse<T>> Execute()
        {
            return await Execute(null);
        }

        /// <summary> 
        /// Execute request
        /// </summary> 
        /// <param name="values">List of parameters for request (content)</param> 
        /// <returns>RestResponse</returns> 
        public async Task<RestResponse<T>> Execute(IEnumerable<KeyValuePair<string, string>> values)
        {
            HttpContent content = null;

            if (values != null)
            {
                if (values is Dictionary<string, string>)
                {
                    content = new StringContent(JsonConvert.SerializeObject(values), Encoding.UTF8, "application/json");
                }
                else
                {
                    content = new FormUrlEncodedContent(values);
                }
            }

            HttpResponseMessage message = null;

            switch (_method)
            {
                case Method.GET:
                    message = await _client.GetAsync(_path);
                    break;
                case Method.POST:
                    message = await _client.PostAsync(_path, content);
                    break;
                case Method.PUT:
                    message = await _client.PutAsync(_path, content);
                    break;
                case Method.DELETE:
                    if (content == null)
                        message = await _client.DeleteAsync(_path);
                    else
                        message = await _client.DeleteAsync(_path, content);
                    break;
            }

            RestResponse<T> response = new RestResponse<T>(message);

            return response;

        }

        /// <summary> 
        /// Execute request
        /// </summary> 
        /// <returns>T</returns> 
        public async Task<T> ExecuteContent()
        {
            return await ExecuteContent(null);
        }

        /// <summary> 
        /// Execute request
        /// </summary> 
        /// <param name="values">List of parameters for request (HttpContent = FormUrlEncodedContent)</param> 
        /// <returns>T</returns> 
        public async Task<T> ExecuteContent(IEnumerable<KeyValuePair<string, string>> values)
        {
            RestResponse<T> response = await Execute(values);
            return response.Content;
        }

        /// <summary> 
        /// Execute request
        /// </summary> 
        /// <param name="values">List of parameters for request (HttpContent = StringContent serialize to Json)"</param> 
        /// <returns>T</returns> 
        public async Task<T> ExecuteContent(Dictionary<string, string> values)
        {
            RestResponse<T> response = await Execute(values);
            return response.Content;
        }

    }
}
