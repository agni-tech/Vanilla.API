using Vanilla.Domain.Users.Dtos;
using Vanilla.Domain.Users.Interfaces;
using Vanilla.Shared.Dtos;
using Microsoft.IdentityModel.Tokens;
using MySaviors.Helpers.Extensions;
using System.Globalization;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Principal;
using System.Text;

namespace Vanilla.Domain.Users.Services;

public class AuthService : IAuthService
{
    private readonly AppSettingsDto _appSettings;

    public AuthService(AppSettingsDto appSettings)
    => _appSettings = appSettings;

    private UserResponseDto _user { get; set; }

    public UserResponseDto GetCurrentUser()
    {
        return _user;
    }

    public CultureInfo GetLocale()
    {
        return (_user?.Locale).IsNullOrEmpty() ? new CultureInfo(_appSettings.Locale) : new CultureInfo(_user.Locale);
    }

    public JwtSecurityToken DecodeTokenFromHeader(string header)
    {
        var handler = new JwtSecurityTokenHandler();

        header = header.Replace("Bearer ", "");

        return handler.ReadToken(header) as JwtSecurityToken;
    }

    public void SetCurrentUserFromAuthToken(JwtSecurityToken authToken)
    {
        if (authToken == null) return;

        _user = new UserResponseDto();

        _user.Id = Convert.ToInt32(authToken?.Claims.FirstOrDefault(claim => claim.Type == "Id")?.Value);
        _user.FirstName = authToken?.Claims.FirstOrDefault(claim => claim.Type == "FirstName")?.Value ?? string.Empty;
        _user.LastName = authToken?.Claims.FirstOrDefault(claim => claim.Type == "LastName")?.Value ?? string.Empty;
        _user.Email = authToken?.Claims.FirstOrDefault(claim => claim.Type == "Email")?.Value ?? string.Empty;
        _user.Role = authToken?.Claims.FirstOrDefault(claim => claim.Type == "Role")?.Value ?? string.Empty;
        _user.Locale = authToken?.Claims.FirstOrDefault(claim => claim.Type == "Locale")?.Value ?? string.Empty;

    }

    public TokenResponseDto GetToken(UserResponseDto user)
    {
        if (user == null)
            return new TokenResponseDto { Authenticated = false };

        var tokenHandler = new JwtSecurityTokenHandler();
        var signingKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_appSettings.Secrets.TokenSecurityKey));
        var signingCredentials = new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256Signature);

        var CreatedDate = DateTime.UtcNow;
        var ExpiredDate = CreatedDate.AddMinutes(5000);

        var claimsIdentity = new ClaimsIdentity(new GenericIdentity(user.Id.ToString(), "Login"), new[]
        {
        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString("N")),
        new Claim("Id", user.Id.ToString()),
        new Claim("FirstName", user.FirstName),
        new Claim("LastName", user.LastName),
        new Claim("Email",user.Email),
        new Claim("Role", user.Role),
        new Claim("Locale", user.Locale)
        });

        var securityTokenDescriptor = new SecurityTokenDescriptor()
        {
            Subject = claimsIdentity,
            SigningCredentials = signingCredentials,
            NotBefore = CreatedDate,
            Expires = ExpiredDate
        };

        var plainToken = tokenHandler.CreateToken(securityTokenDescriptor);
        var signedAndEncodedToken = tokenHandler.WriteToken(plainToken);

        return new TokenResponseDto()
        {
            Authenticated = true,
            Created = CreatedDate.ToString("yyyy-MM-dd HH:mm:ss"),
            Expiration = ExpiredDate.ToString("yyyy-MM-dd HH:mm:ss"),
            AccessToken = signedAndEncodedToken
        };
    }
}
