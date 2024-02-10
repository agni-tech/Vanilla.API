using AutoMapper;
using Vanilla.Domain.Common.Interfaces;
using Vanilla.Shared.Dtos;
using Vanilla.Shared.Service;
using MySaviors.Helpers.Extensions;

namespace Vanilla.Domain.Common.Services;

public class AddressService : IAddressService
{
    readonly IMapper _mapper;
    readonly IZipCodeService _zipCodeService;

    public AddressService(IMapper mapper, IZipCodeService zipCodeService)
    {
        _mapper = mapper;
        _zipCodeService = zipCodeService;
    }

    public async Task<AddressDto> GetASync(string ZipCode)
    {
        var result = await _zipCodeService.GetAddressAsync(ZipCode);

        if (result.IsNotNull() && result.httpStatusCode == System.Net.HttpStatusCode.OK)
            return _mapper.Map<AddressDto>(result.zipCodeResponse);

        return null;

    }
}