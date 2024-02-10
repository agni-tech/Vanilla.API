using Vanilla.Domain.UserGroups.Entities;
using Vanilla.Shared.Repository;

namespace Vanilla.Domain.UserGroups.Interfaces;

public interface IUserGroupRepository : IBaseRepository<int, UserGroup>
{
    Task<UserGroup>  GetById(int id);
}
