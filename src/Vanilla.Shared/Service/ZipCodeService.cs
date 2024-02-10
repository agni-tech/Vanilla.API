using Vanilla.Shared.Dtos;
using Vanilla.Shared.Interfaces;
using System.Net;
using System.Text.Json;

namespace Vanilla.Shared.Service;

public class ZipCodeService : IZipCodeService
{
    private AppSettingsDto _appSettings;
    private readonly IHttpClientHelper _httpClient;

    public ZipCodeService(AppSettingsDto appSettings, IHttpClientHelper httpClient)
    {
        _appSettings = appSettings;
        _httpClient = httpClient;
    }

    public async Task<(ZipCodeResponseDto zipCodeResponse, HttpStatusCode httpStatusCode)> GetAddressAsync(string zipCode)
    {
        var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
        var apiUrl = _appSettings.Links.ZipCodeSearch.ToLower().Replace("{cep}", zipCode);
        var response = await _httpClient.GetAsync(apiUrl);
        ZipCodeResponseDto result = null;


        if (response.StatusCode == HttpStatusCode.OK)
        {
            var buffer = await response.Content.ReadAsStringAsync();
            result = JsonSerializer.Deserialize<ZipCodeResponseDto>(buffer, options);
        }

        return (result, response.StatusCode);
    }
}

