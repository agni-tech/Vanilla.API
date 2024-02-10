using Vanilla.Domain.UserGroups.Dtos;
using Vanilla.Shared.Service;

namespace Vanilla.Domain.UserGroups.Interfaces
{
    public interface IUserGroupService : IBaseService<int, UserGroupDto, UserGroupResponseDto>
    { 
    }
}
