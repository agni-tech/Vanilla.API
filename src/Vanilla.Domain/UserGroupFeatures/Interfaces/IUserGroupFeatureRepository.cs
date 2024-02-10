using Vanilla.Domain.UserGroupFeatures.Entities;
using Vanilla.Domain.UserGroups.Entities;
using Vanilla.Shared.Repository;

namespace Vanilla.Domain.UserGroupFeatures.Interfaces;

public interface IUserGroupFeatureRepository : IBaseRepository<int, UserGroupFeature>
{
    Task<UserGroupFeature>  GetById(int id);
}
