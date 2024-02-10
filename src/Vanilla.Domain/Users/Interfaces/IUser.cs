namespace Vanilla.Domain.Users.Interfaces
{
    public interface IUser
    {
        string FirstName { get; set; }
        string LastName { get; set; }
        string Email { get; set; }
        string Role { get; set; }
    }
}
