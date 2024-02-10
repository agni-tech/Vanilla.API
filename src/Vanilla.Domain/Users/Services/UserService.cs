using AutoMapper;
using Vanilla.Domain.Common.Interfaces;
using Vanilla.Domain.User.Interfaces;
using Vanilla.Domain.Users.Dtos;
using Vanilla.Domain.Users.Interfaces;
using Vanilla.Shared.Extensions;
using Vanilla.Shared.Helpers;
using Microsoft.IdentityModel.Tokens;
using MySaviors.Helpers.Extensions;

namespace Vanilla.Domain.Users.Services;

public class UserService : IUserService
{
    private readonly IUserRepository _repository;
    private readonly IMapper _mapper;
    private readonly IEmailService _emailService;

    public UserService(IUserRepository repository, IMapper mapper, IEmailService emailService)
        => (_repository, _mapper, _emailService) = (repository, mapper, emailService);

    public async Task<List<UserResponseDto>> GetAsync()
        => _mapper.Map<List<UserResponseDto>>(await _repository.ToListAsync());

    public async Task<UserResponseDto> GetAsync(int id)
        => _mapper.Map<UserResponseDto>(await _repository.GetByIdAsync(id));

    public async Task<UserResponseDto> AddAsync(UserDto dto)
    {
        var result = _mapper.Map<Entities.User>(dto);

        if (await Validate(result))
            return _mapper.Map<UserResponseDto>(await _repository.AddAsync(result));

        return _mapper.Map<UserResponseDto>(result);
    }

    public async Task<UserResponseDto> UpdateAsync(int id, UserDto dto)
    {
        var result = _mapper.Map<Entities.User>(await _repository.GetByIdAsync(id));

        result.FirstName = dto.FirstName;
        result.LastName = dto.LastName;
        result.Email = dto.Email;
        result.Role = dto.Role;

        if (result.Validate())
            _repository.Update(result);

        return _mapper.Map<UserResponseDto>(result);
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var result = await _repository.GetByIdAsync(id);
        if (result == null) return false;
        _repository.Remove(result);
        return true;
    }

    public async Task<UserResponseDto> GetByEmailPasswordAsync(string email, string password)
    {
        if (!ValidateUserRequest(email, password))
            return null;

        var result = await _repository.GetUserByEmailPassword(email, password.MakeHash());
        return _mapper.Map<UserResponseDto>(result);
    }

    private bool ValidateUserRequest(string email, string password)
    {

        var list = Notifications.Init()
                           .When(email.IsNullOrEmpty(), "EMAIL_ISNULL_OR_EMPTY")
                           .When(!email.IsEmail(), "EMAIL_INVALID")
                           .When(password.IsNullOrEmpty(), "PASSWORD_ISNULL_OR_EMPTY")
                    .Messages;

        if (list.HaveAny())
            NotificationsWrapper.AddMessage(list);

        return NotificationsWrapper.IsValid();
    }

    private async Task<bool> Validate(Entities.User user)
    {
        if (!user.Validate())
            return false;

        var repo = await _repository.GetAsyncAsNoTracking(q => q.Email == user.Email);
        if (repo.HaveAny())
        {
            NotificationsWrapper.AddMessage("EMAIL_ALREADY_EXISTS");
            return false;
        }
        return true;
    }

    public async Task<bool> SendResetEmailAsync(string email)
    {
        var result = (await _repository.GetAsyncAsNoTracking(q => q.Email == email)).FirstOrDefault();

        if (result.IsNull())
            return false;

        result.RecoveryCode = Guid.NewGuid().ToString();
        result.RecoveryCodeExpiration = DateTime.Now.AddMinutes(15);

        _repository.Update(result);

        return await _emailService.SendTemplate(result.Email, "PASSWORD_RECOVERY", "reset-pwd", result, result.Locale);
    }

    public async Task<bool> ChangeRecoveryPasswordAsync(string code, string password)
    {
        var result = (await _repository.GetAsyncAsNoTracking(q => q.RecoveryCode == code)).FirstOrDefault();

        if (result.IsNotNull() || (result.RecoveryCodeExpiration.IsNotNull() && result.RecoveryCodeExpiration.Value <= DateTime.Now))
        {

            result.Password = password.MakeHash();
            result.RecoveryCode = null;
            result.RecoveryCodeExpiration = null;

            _repository.Update(result);

            return true;
        }

        return false;
    }
}