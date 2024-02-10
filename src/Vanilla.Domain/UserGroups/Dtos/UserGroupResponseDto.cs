using Vanilla.Shared.Dtos;

namespace Vanilla.Domain.UserGroups.Dtos;

public class UserGroupResponseDto : BaseResponseDto<int>
{
    public string Name { get; set; }
    public string Description { get; set; }
}
