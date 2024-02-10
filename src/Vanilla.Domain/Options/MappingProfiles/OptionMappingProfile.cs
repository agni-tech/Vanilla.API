using AutoMapper;
using Vanilla.Domain.Options.Dtos;
using Vanilla.Domain.Options.Entities;
namespace Vanilla.Domain.Options.MappingProfiles;
public class OptionMappingProfile : Profile
{
    public OptionMappingProfile()
    {
        CreateMap<Option, OptionDto>()
            .ReverseMap()
            .PreserveReferences();

        CreateMap<OptionDto, Option>()
            .ReverseMap()
            .PreserveReferences();

        CreateMap<Option, OptionResponseDto>()
            .ForMember(_ => _.Errors, x => x.MapFrom(_ => _.ValidationResult.Errors))
            .PreserveReferences();

        CreateMap<OptionResponseDto, OptionDto>()
            .ReverseMap()
            .PreserveReferences();
        }
}
