using Vanilla.Domain.UserGroupFeatures.Entities;
using Vanilla.Domain.UserGroupFeatures.Interfaces;
using Vanilla.Infra.Data.Contexts;
using Vanilla.Shared.Repository;
using Microsoft.EntityFrameworkCore;

namespace Vanilla.Infra.Data.Repositories;

public class UserGroupFeatureRepository : BaseRepository<int, UserGroupFeature>, IUserGroupFeatureRepository
{
    public UserGroupFeatureRepository(VanillaContext context) : base(context)
    {
    }
    public async Task<UserGroupFeature>  GetById(int id)
    {
        return await _dbSet.Where(q => q.Id == id).FirstOrDefaultAsync();
    }
}
