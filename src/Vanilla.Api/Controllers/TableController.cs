using Vanilla.API.Dtos;
using Vanilla.API.Helpers;
using Vanilla.Shared.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Vanilla.API.Controllers;

[ApiController]
[Route("table")]
public class TableController : ControllerBase
{

    private readonly IControllerHelper _controllerHelper;

    public TableController(IControllerHelper controllerHelper)
        => (_controllerHelper) = (controllerHelper);

    [Authorize("Bearer")]
    [HttpGet("{table}")]
    public async Task<IActionResult> Get([FromRoute] string table)
    {
        var request = TableServiceFactory.GetInstance(table);
        return await _controllerHelper.PackResultAsync(() => request.service.GetAsync());
    }


    [Authorize("Bearer")]
    [HttpGet("{id}/{table}")]
    public async Task<IActionResult> Get([FromRoute] int id, [FromRoute] string table)
    {
        var request = TableServiceFactory.GetInstance(table);
        return await _controllerHelper.PackResultAsync(() => request.service.GetAsync(id));
    }

    [Authorize("Bearer")]
    [HttpPost("{table}")]
    public async Task<IActionResult> Post([FromBody] TableDto dto, [FromRoute] string table)
    {
        var request = TableServiceFactory.GetInstance(table);
        request.dto.Name = dto.Name;
        request.dto.Description = dto.Description;

        return await _controllerHelper.PackResultAsync(() => request.service.AddAsync(request.dto));
    }

    [Authorize("Bearer")]
    [HttpPut("{id}/{table}")]
    public async Task<IActionResult> Put([FromRoute] int id, [FromRoute] string table, [FromBody] TableDto dto)
    {
        var request = TableServiceFactory.GetInstance(table);
        request.dto.Name = dto.Name;
        request.dto.Description = dto.Description;

        return await _controllerHelper.PackResultAsync(() => request.service.UpdateAsync(id, request.dto));
    }

    [Authorize("Bearer")]
    [HttpDelete("{id}/{table}")]
    public async Task<IActionResult> Delete([FromRoute] int id, [FromRoute] string table)
    {
        var request = TableServiceFactory.GetInstance(table);
        return await _controllerHelper.PackResultAsync(() => request.service.DeleteAsync(id));
    }

    
}