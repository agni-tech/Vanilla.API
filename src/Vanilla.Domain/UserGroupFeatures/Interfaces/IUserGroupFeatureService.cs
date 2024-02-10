using Vanilla.Domain.UserGroupFeatures.Dtos;
using Vanilla.Shared.Service;

namespace Vanilla.Domain.UserGroupFeatures.Interfaces;

public interface IUserGroupFeatureService : IBaseService<int, UserGroupFeatureDto, UserGroupFeatureResponseDto>
{
}
