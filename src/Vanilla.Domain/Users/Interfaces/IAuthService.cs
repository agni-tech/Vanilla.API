using System.Globalization;
using System.IdentityModel.Tokens.Jwt;
using Vanilla.Domain.Users.Dtos;

namespace Vanilla.Domain.Users.Interfaces;

public interface IAuthService
{
    UserResponseDto GetCurrentUser();
    JwtSecurityToken DecodeTokenFromHeader(string header);
    void SetCurrentUserFromAuthToken(JwtSecurityToken authToken);
    TokenResponseDto GetToken(UserResponseDto user);
    public CultureInfo GetLocale();
}
