using Vanilla.Shared.Dtos;

namespace Vanilla.Domain.Users.Dtos;

public class UserResponseDto : BaseResponseDto<int>
{
    public int? EnterpriseId { get; set; }
    public int? UserGroupId { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string Role { get; set; }
    public string Locale { get; set; }
}
