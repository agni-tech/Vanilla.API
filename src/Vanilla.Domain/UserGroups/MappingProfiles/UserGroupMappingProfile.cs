using AutoMapper;
using Vanilla.Domain.UserGroups.Dtos;

namespace Vanilla.Domain.UserGroups.MappingProfiles;

public class UserGroupMappingProfile : Profile
{
    public UserGroupMappingProfile() {

        CreateMap<Entities.UserGroup, UserGroupDto>()
               .ReverseMap()
               .PreserveReferences();

        CreateMap<UserGroupDto, Entities.UserGroup>()
            .ReverseMap()
            .PreserveReferences();

        CreateMap<Entities.UserGroup, UserGroupResponseDto>()
            .ForMember(_ => _.Errors, x => x.MapFrom(_ => _.ValidationResult.Errors))
            .PreserveReferences();

        CreateMap<UserGroupResponseDto, UserGroupDto>()
            .ReverseMap()
            .PreserveReferences();
    }
}
