using Vanilla.Domain.User.Interfaces;
using Vanilla.Domain.Users.Entities;
using Vanilla.Infra.Data.Contexts;
using Vanilla.Shared.Repository;
using Microsoft.EntityFrameworkCore;

namespace Vanilla.Infra.Data.Repositories;

public class UserRepository : BaseRepository<int, User>, IUserRepository
{
    public UserRepository(VanillaContext context) : base(context)
    {
    }

    public async Task<User> GetUserByEmailPassword(string email, string password)
    {
       return await _dbSet.Where(q => q.Email.Equals(email) && q.Password.Equals(password))
                          .FirstOrDefaultAsync();
    }
}
