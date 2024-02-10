using Vanilla.Shared.Repository;

namespace Vanilla.Domain.User.Interfaces;

public interface IUserRepository : IBaseRepository<int, Users.Entities.User>
{
    Task<Users.Entities.User> GetUserByEmailPassword(string email, string password);
}
