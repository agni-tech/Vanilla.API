using Vanilla.API.Helpers;
using Vanilla.Domain.UserGroupFeatures.Dtos;
using Vanilla.Domain.UserGroupFeatures.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Vanilla.API.Controllers;

[ApiController]
[Route("usergroupfeature")]
public class UserGroupFeatureController : ControllerBase
{
    private readonly IControllerHelper _controllerHelper;
    private readonly IUserGroupFeatureService _userGroupFeatureService;

    public UserGroupFeatureController(IUserGroupFeatureService userGroupFeatureService, IControllerHelper controllerHelper)
    {
        _userGroupFeatureService = userGroupFeatureService;
        _controllerHelper = controllerHelper;
    }

    [Authorize("Bearer")]
    [HttpGet]
    public async Task<IActionResult> Get()
    {
        return await _controllerHelper.PackResultAsync(() => _userGroupFeatureService.GetAsync());
    }

    [Authorize("Bearer")]
    [HttpGet("{id}")]
    public async Task<IActionResult> Get([FromRoute] int id)
    {
        return await _controllerHelper.PackResultAsync(() => _userGroupFeatureService.GetAsync(id));
    }

    [Authorize("Bearer")]
    [HttpPost]
    public async Task<IActionResult> Post([FromBody] UserGroupFeatureDto dto)
    {
        return await _controllerHelper.PackResultAsync(() => _userGroupFeatureService.AddAsync(dto));
    }

    [Authorize("Bearer")]
    [HttpPut("{id}")]
    public async Task<IActionResult> Put([FromRoute] int id, [FromBody] UserGroupFeatureDto dto)
    {
        return await _controllerHelper.PackResultAsync(() => _userGroupFeatureService.UpdateAsync(id, dto));
    }

    [Authorize("Bearer")]
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete([FromRoute] int id)
    {
        return await _controllerHelper.PackResultAsync(() => _userGroupFeatureService.DeleteAsync(id));
    }
}