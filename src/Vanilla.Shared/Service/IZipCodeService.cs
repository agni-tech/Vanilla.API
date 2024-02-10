using Vanilla.Shared.Dtos;
using System.Net;

namespace Vanilla.Shared.Service;

public interface IZipCodeService
{
    Task<(ZipCodeResponseDto zipCodeResponse, HttpStatusCode httpStatusCode)> GetAddressAsync(string zipCode);
}
