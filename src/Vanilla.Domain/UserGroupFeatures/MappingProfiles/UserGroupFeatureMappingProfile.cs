using AutoMapper;
using Vanilla.Domain.UserGroupFeatures.Dtos;
using Vanilla.Domain.UserGroupFeatures.Entities;
namespace Vanilla.Domain.UserGroupFeatures.MappingProfiles;

public class UserGroupFeatureMappingProfile : Profile
{
    public UserGroupFeatureMappingProfile()
    {

        CreateMap<UserGroupFeature, UserGroupFeatureDto>()
            .ReverseMap()
             .PreserveReferences();

        CreateMap<UserGroupFeatureDto, UserGroupFeature>()
            .ReverseMap()
            .PreserveReferences();

        CreateMap<UserGroupFeatureDto, UserGroupFeature>()
            .ReverseMap()
            .PreserveReferences();

        CreateMap<UserGroupFeature, UserGroupFeatureResponseDto>()
            .ForMember(_ => _.Errors, x => x.MapFrom(_ => _.ValidationResult.Errors))
            .PreserveReferences();

        CreateMap<UserGroupFeatureResponseDto, UserGroupFeatureDto>()
            .ReverseMap()
            .PreserveReferences();
    }
}