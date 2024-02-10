using Vanilla.Domain.Users.Interfaces;
using Microsoft.AspNetCore.Http;

namespace Vanilla.Domain.Users.Services.Middlewares
{
    public class AuthMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IAuthService _authService;

        public AuthMiddleware(RequestDelegate next, IAuthService authService)
        {
            _next = next;
            _authService = authService;
        }

        public async Task Invoke(HttpContext context)
        {
            var authHeader = context.Request.Headers["Authorization"].ToString();

            if (!string.IsNullOrEmpty(authHeader))
            {
                var token = _authService.DecodeTokenFromHeader(authHeader);

                _authService.SetCurrentUserFromAuthToken(token);
            }

            await _next.Invoke(context);
        }
    }
}
