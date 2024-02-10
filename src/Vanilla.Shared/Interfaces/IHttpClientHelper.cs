using System.Net.Http.Headers;

namespace Vanilla.Shared.Interfaces;

public interface IHttpClientHelper
{
    void SetRequestHeaderAuth(AuthenticationHeaderValue value);
    HttpResponseMessage Get(string url);
    Task<HttpResponseMessage> GetAsync(string url);
    HttpResponseMessage Post(string url, HttpContent content);
    Task<HttpResponseMessage> PostAsync(string url, HttpContent content);
}
