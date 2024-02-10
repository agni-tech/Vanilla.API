using AutoMapper;
using Vanilla.Shared.Dtos;
using System.Net;

namespace Vanilla.Domain.Activities.MappingProfiles;

public class AddressMappingProfile : Profile
{
    public AddressMappingProfile()
    {
        CreateMap<ZipCodeResponseDto, AddressDto>()
            .ForMember(_ => _.ZipCode, x => x.MapFrom(_ => _.Cep))
            .ForMember(_ => _.Street, x => x.MapFrom(_ => _.Logradouro))
            .ForMember(_ => _.Neighborhood, x => x.MapFrom(_ => _.Bairro))
            .ForMember(_ => _.City, x => x.MapFrom(_ => _.Localidade))
            .ForMember(_ => _.State, x => x.MapFrom(_ => _.Uf))
            .ReverseMap()
            .PreserveReferences();

      
    }
}