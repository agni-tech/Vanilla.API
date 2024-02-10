using Vanilla.Shared.Dtos;
using Vanilla.Shared.Interfaces;
using System.Net;
using System.Text.Json;

namespace Vanilla.Shared.Service;

public class RedemetService : IRedemetService
{
    private AppSettingsDto _appSettings;
    private readonly IHttpClientHelper _httpClient;

    public RedemetService(AppSettingsDto appSettings, IHttpClientHelper httpClient)
    {
        _appSettings = appSettings;
        _httpClient = httpClient;
    }

    public async Task<(AerodromoRedemetDto response, HttpStatusCode httpStatusCode)> GetAsync(string local, DateTime date)
    {
        var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
        var apiUrl = $"{_appSettings.Links.Redemet}?api_key={_appSettings.Secrets.RedemetApiKey}&localidade={local}&datahora={date.ToString("yyyyMMddHH")}";
        var response = await _httpClient.GetAsync(apiUrl);
        AerodromoRedemetDto result = null;


        if (response.StatusCode == HttpStatusCode.OK)
        {
            var buffer = await response.Content.ReadAsStringAsync();
            result = JsonSerializer.Deserialize<AerodromoRedemetDto>(buffer, options);
        }

        return (result, response.StatusCode);
    }
}

