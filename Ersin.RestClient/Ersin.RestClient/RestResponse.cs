using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json;

namespace Ersin.RestClient
{
    /// <summary> 
    /// Class for response
    /// </summary> 
    public class RestResponse<T> : IRestResponse<T>
    {

        public HttpResponseHeaders Headers { get; set; }

        public bool IsSuccessStatusCode { get; set; }

        public string ReasonPhrase { get; set; }

        public HttpStatusCode StatusCode { get; set; }

        public string Raw { get; set; }

        public T Content { get; set; }

        /// <summary> 
        /// Default constructor
        /// </summary> 
        /// <param name="message">Http Response Message from Http request</param> 
        public RestResponse(HttpResponseMessage message)
        {

            if (message == null)
                throw new ArgumentNullException(nameof(message));

            Headers = message.Headers;
            IsSuccessStatusCode = message.IsSuccessStatusCode;
            ReasonPhrase = message.ReasonPhrase;
            StatusCode = message.StatusCode;

            GetContent(message);
        }

        private async void GetContent(HttpResponseMessage message)
        {
            Raw = await message.Content.ReadAsStringAsync();

            if (typeof(T) == typeof(String))
                Content = (T)Convert.ChangeType(Raw, typeof(T));
            else
                Content = JsonConvert.DeserializeObject<T>(Raw);
        }

    }
}
