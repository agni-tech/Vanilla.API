using Vanilla.Domain.Users.Dtos;
using Vanilla.Shared.Service;

namespace Vanilla.Domain.Users.Interfaces;

public interface IUserService : IBaseService<int, UserDto, UserResponseDto>
{
    Task<UserResponseDto> GetByEmailPasswordAsync(string email, string password);
    Task<bool> SendResetEmailAsync(string email);
    Task<bool> ChangeRecoveryPasswordAsync(string code, string password);
    Task<bool> DeleteAsync(int id);
}
