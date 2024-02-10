using Microsoft.AspNetCore.Mvc;

namespace Vanilla.API.Controllers
{
    [ApiController]
    [Route("health")]
    public class HealthController : ControllerBase
    {

        private readonly ILogger<HealthController> _logger;

        public HealthController(ILogger<HealthController> logger)
        {
            _logger = logger;
        }

        [HttpGet(Name = "Health")]
        public IActionResult Get()
        {
            return Ok(true);
        }
    }
}