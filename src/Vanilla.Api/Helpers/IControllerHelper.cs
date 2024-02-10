using Microsoft.AspNetCore.Mvc;

namespace Vanilla.API.Helpers;

public interface IControllerHelper
{
    public ObjectResult PackResult(Func<dynamic> procedure);
    Task<ObjectResult> PackResultAsync(Func<dynamic> procedure);
}
