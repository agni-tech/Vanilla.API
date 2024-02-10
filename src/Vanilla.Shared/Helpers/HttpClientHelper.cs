using Vanilla.Shared.Interfaces;
using System.Net.Http.Headers;

namespace Vanilla.Shared.Helpers;

public class HttpClientHelper : IHttpClientHelper, IDisposable
{
    private readonly HttpClient _client;

    public HttpClientHelper() => _client = new HttpClient();

    public void Dispose()
        => _client.Dispose();

    public void SetRequestHeadersAuthorization(AuthenticationHeaderValue value)
        => _client.DefaultRequestHeaders.Authorization = value;

    public HttpResponseMessage Get(string url)
        => GetAsync(url).Result;

    public async Task<HttpResponseMessage> GetAsync(string url)
        => await _client.GetAsync(url);

    public HttpResponseMessage Post(string url, HttpContent content)
        => PostAsync(url, content).Result;

    public async Task<HttpResponseMessage> PostAsync(string url, HttpContent content)
        => await _client.PostAsync(url, content);

    public void SetRequestHeaderAuth(AuthenticationHeaderValue value)
        => throw new NotImplementedException();
}
