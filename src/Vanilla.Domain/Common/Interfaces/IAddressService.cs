using Vanilla.Shared.Dtos;

namespace Vanilla.Domain.Common.Interfaces;

public interface IAddressService
{
    Task<AddressDto> GetASync(string ZipCode);
}
