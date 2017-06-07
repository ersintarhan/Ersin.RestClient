using System.Net.Http;
using System.Threading.Tasks;

namespace Ersin.RestClient
{
    public interface IRestClient
    {

        void AddHeader(string name, string value);


        HttpResponseMessage Get(string path);
        Task<HttpResponseMessage> GetAsync(string path);
        HttpResponseMessage Post(string path, HttpContent content);
        Task<HttpResponseMessage> PostAsync(string path, HttpContent content);
        HttpResponseMessage Put(string path, HttpContent content);
        Task<HttpResponseMessage> PutAsync(string path, HttpContent content);
        HttpResponseMessage Delete(string path);
        Task<HttpResponseMessage> DeleteAsync(string path);
        HttpResponseMessage Delete(string path, HttpContent content);
        Task<HttpResponseMessage> DeleteAsync(string path, HttpContent content);

    }


  

}
