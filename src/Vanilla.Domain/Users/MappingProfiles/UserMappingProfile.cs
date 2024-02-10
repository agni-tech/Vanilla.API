using AutoMapper;
using Vanilla.Domain.Users.Dtos;
using Vanilla.Shared.Extensions;

namespace Vanilla.Domain.Users.MappingProfiles;

public class UserMappingProfile : Profile
{
    public UserMappingProfile()
    {
        CreateMap<Entities.User, UserDto>()
            .ReverseMap()
            .PreserveReferences();

        CreateMap<UserDto, Entities.User>()
            .ForMember(_ => _.Password, x => x.MapFrom(_ => _.Password.MakeHash()))
            .ReverseMap()
            .PreserveReferences();

        CreateMap<Entities.User, UserResponseDto>()
            .ForMember(_ => _.Errors, x => x.MapFrom(_ => _.ValidationResult.Errors))
            .PreserveReferences();

        CreateMap<UserResponseDto, UserDto>()
            .ReverseMap()
            .PreserveReferences();
    }
}
