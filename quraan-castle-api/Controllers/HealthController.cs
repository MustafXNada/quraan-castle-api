using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace quraan_castle_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HealthController : ControllerBase
    {
        [HttpGet("/health")]
        public IActionResult Get()
        {
            return Ok("health");
        }
    }
}
