using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;

namespace Vanilla.Domain.Users.Services.Middlewares
{
    public class LocalAndDevAuthMock
    {
        private readonly RequestDelegate _next;
        private readonly IConfiguration _configuration;

        public LocalAndDevAuthMock(RequestDelegate next, IConfiguration configuration)
        {
            _next = next;
            _configuration = configuration;
        }

        public async Task Invoke(HttpContext context)
        {
            if (!context.Request.Headers.Any(x => x.Key == "Authorization"))
                context.Request.Headers.Add("Authorization", $"Bearer {_configuration.GetSection("LocalAndDevAuthTokenMock").Value}");

            await _next.Invoke(context);
        }
    }
}
