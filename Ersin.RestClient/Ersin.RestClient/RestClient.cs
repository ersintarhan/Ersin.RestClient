using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace Ersin.RestClient
{
    
    public class RestClient : IRestClient
    {

        private readonly HttpClient httpClient;
        private readonly string url;
        private readonly string headersMediaType = "application/json";

        public AuthenticationHeaderValue Authorization { get; set; }

        public RestClient(string baseUrl)
        {
            url = baseUrl;

            httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Clear();
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(headersMediaType));
        }

        public async Task<HttpResponseMessage> GetAsync(string path)
        {
            if (Authorization != null)
                httpClient.DefaultRequestHeaders.Authorization = Authorization;

            return await httpClient.GetAsync(Helper.GetAddress(url, path));
        }

        public async Task<HttpResponseMessage> PostAsync(string path, HttpContent content)
        {
            if (Authorization != null)
                httpClient.DefaultRequestHeaders.Authorization = Authorization;

            return await httpClient.PostAsync(Helper.GetAddress(url, path), content);
        }

        public async Task<HttpResponseMessage> PutAsync(string path, HttpContent content)
        {

            if (Authorization != null)
                httpClient.DefaultRequestHeaders.Authorization = Authorization;

            return await httpClient.PutAsync(Helper.GetAddress(url, path), content);
        }

        public async Task<HttpResponseMessage> DeleteAsync(string path)
        {
            if (Authorization != null)
                httpClient.DefaultRequestHeaders.Authorization = Authorization;

            return await httpClient.DeleteAsync(Helper.GetAddress(url, path));
        }

        public async Task<HttpResponseMessage> DeleteAsync(string path, HttpContent content)
        {
            if (Authorization != null)
                httpClient.DefaultRequestHeaders.Authorization = Authorization;

            return await httpClient.SendAsync(new HttpRequestMessage()
            {
                RequestUri = new Uri(Helper.GetAddress(url, path)),
                Content = content,
                Method = HttpMethod.Delete
            });
        }

        public void AddHeader(string name, string value)
        {
            httpClient.DefaultRequestHeaders.Add(name, value);
        }

        public HttpResponseMessage Get(string path)
        {
            return this.GetAsync(path).Result;
        }

        public HttpResponseMessage Post(string path, HttpContent content)
        {
            return this.PostAsync(path, content).Result;
        }

        public HttpResponseMessage Put(string path, HttpContent content)
        {
            return this.PutAsync(path, content).Result;

        }

        public HttpResponseMessage Delete(string path)
        {
            return this.DeleteAsync(path).Result;
        }

        public HttpResponseMessage Delete(string path, HttpContent content)
        {
            return this.DeleteAsync(path, content).Result;
        }
    }
}
