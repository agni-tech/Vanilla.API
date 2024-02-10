using Vanilla.API.Helpers;
using Vanilla.Domain.Users.Dtos;
using Vanilla.Domain.Users.Interfaces;
using Vanilla.Shared.Helpers;
using Vanilla.Shared.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Globalization;

namespace Vanilla.API.Controllers
{
    [ApiController]
    [Route("auth")]
    public class AuthController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IAuthService _authService;
        private readonly ILocalizationHelper _localizationHelper;
        private readonly IControllerHelper _controllerHelper;
        private readonly CultureInfo _culture;

        public AuthController(IUserService userService, IAuthService authService, ILocalizationHelper localizationHelper
             , IControllerHelper controllerHelper)
        {
            _userService = userService;
            _authService = authService;
            _localizationHelper = localizationHelper;
            _controllerHelper = controllerHelper;
            _culture = _authService.GetLocale();
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] TokenDto login)
        {
            var user = await _userService.GetByEmailPasswordAsync(login.Email, login.Password);
            if (user != null)
                return await _controllerHelper.PackResultAsync(() => Task.FromResult(_authService.GetToken(user)));
            else
                NotificationsWrapper.AddMessage(_localizationHelper.GetString("LOGIN_EXCEPTION", _culture));

            return await _controllerHelper.PackResultAsync(() => null);
        }

        [AllowAnonymous]
        [HttpPost("reset/{email}")]
        public async Task<IActionResult> Post(string email)
        {
            return await _controllerHelper.PackResultAsync(() => _userService.SendResetEmailAsync(email));
        }
        [AllowAnonymous]
        [HttpPost("reset")]
        public async Task<IActionResult> Post(string code, string password)
        {
            return await _controllerHelper.PackResultAsync(() => _userService.ChangeRecoveryPasswordAsync(code, password));
        }
    }
}