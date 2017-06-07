using System.Net;
using System.Net.Http.Headers;

namespace Ersin.RestClient
{
    public interface IRestResponse
    {

        HttpResponseHeaders Headers { get; set; }

        bool IsSuccessStatusCode { get; set; }

        string ReasonPhrase { get; set; }

        HttpStatusCode StatusCode { get; set; }

        string Raw { get; set; }

    }

    public interface IRestResponse<T> : IRestResponse
    {
        T Content { get; set; }
    }
}