using Vanilla.Domain.UserGroups.Entities;
using Vanilla.Domain.UserGroups.Interfaces;
using Vanilla.Infra.Data.Contexts;
using Vanilla.Shared.Repository;
using Microsoft.EntityFrameworkCore;

namespace Vanilla.Infra.Data.Repositories;

public class UserGroupRepository : BaseRepository<int, UserGroup>, IUserGroupRepository
{
    public UserGroupRepository(VanillaContext context) : base(context)
    {
    }
    public async Task<UserGroup>  GetById(int id)
    {
        return await _dbSet.Where(q => q.Id == id).FirstOrDefaultAsync();
    }
}
