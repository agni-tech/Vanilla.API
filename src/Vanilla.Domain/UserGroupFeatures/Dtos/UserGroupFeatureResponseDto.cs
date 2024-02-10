using Vanilla.Shared.Dtos;

namespace Vanilla.Domain.UserGroupFeatures.Dtos;

public class UserGroupFeatureResponseDto : BaseResponseDto<int>
{
    public int UserGroupId { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
}
