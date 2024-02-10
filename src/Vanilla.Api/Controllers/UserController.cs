using Vanilla.API.Helpers;
using Vanilla.Domain.Users.Dtos;
using Vanilla.Domain.Users.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Vanilla.API.Controllers;

[ApiController]
[Route("user")]
public class UserController : ControllerBase
{

    private readonly IControllerHelper _controllerHelper;
    private readonly IUserService _userService;

    public UserController(IUserService userService, IControllerHelper controllerHelper)
        => (_userService, _controllerHelper) = (userService, controllerHelper);

    [Authorize("Bearer")]
    [HttpGet]
    public async Task<IActionResult> Get()
        => await _controllerHelper.PackResultAsync(() => _userService.GetAsync());

    [Authorize("Bearer")]
    [HttpGet("{id}")]
    public async Task<IActionResult> Get([FromRoute] int id)
        => await _controllerHelper.PackResultAsync(() => _userService.GetAsync(id));

    [AllowAnonymous]
    [HttpPost]
    public async Task<IActionResult> Post([FromBody] UserDto dto)
        => await _controllerHelper.PackResultAsync(() => _userService.AddAsync(dto));

    [Authorize("Bearer")]
    [HttpPut("{id}")]
    public async Task<IActionResult> Put([FromRoute] int id, [FromBody] UserDto dto) 
        => await _controllerHelper.PackResultAsync(() => _userService.UpdateAsync(id, dto));

    [Authorize("Bearer")]
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete([FromRoute] int id) 
        => await _controllerHelper.PackResultAsync(() => _userService.DeleteAsync(id));
}