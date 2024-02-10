namespace Vanilla.Domain.Users.Dtos;

public class UserDto
{
    public int? EnterpriseId { get; set; }
    public int? UserGroupId { get; set; }
    public int UserId { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string Role { get; set; }
    public string Locale { get; set; }
    public string Password { get; set; }

}
