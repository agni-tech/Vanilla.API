using Vanilla.API.Helpers;
using Vanilla.Domain.Common.Interfaces;
using Vanilla.Domain.Users.Interfaces;
using Vanilla.Shared.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MySaviors.Helpers.Extensions;

namespace Vanilla.API.Controllers;

[ApiController]
[Route("config")]
public class ConfigController : ControllerBase
{
    private readonly IFileService _fileService;
    private readonly IAddressService _zipCodeService;
    private readonly IAuthService _authService;
    private readonly IControllerHelper _controllerHelper;

    public ConfigController(IAddressService zipCodeService, IFileService fileService, IAuthService authService, IControllerHelper controllerHelper)
        => (_zipCodeService, _fileService, _authService, _controllerHelper) = (zipCodeService, fileService, authService, controllerHelper);

    [Authorize("Bearer")]
    [HttpGet("zipCode/{zipCode}")]
    public async Task<IActionResult> Get(string zipCode)
    => await _controllerHelper.PackResultAsync(() => _zipCodeService.GetASync(zipCode));

    //[Authorize("Bearer")]
    //[HttpPost("upload/metering-file")]
    //public async Task<IActionResult> UploadAsync(IFormFile file)
    //{

    //    if (file == null)
    //        return _controllerHelper.PackResult(null);

    //    return await _controllerHelper.PackResultAsync(() =>
    //    {
    //        var user = _authService.GetCurrentUser();
    //        var result = _fileService.GetMeteringFile(file, 5);
    //        if (result.IsNull()) return null;
    //        return _meteringService.SaveMeteringFile(result, user.Id);
    //    });
    //}
}
