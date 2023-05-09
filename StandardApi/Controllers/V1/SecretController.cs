using Microsoft.AspNetCore.Mvc;
using StandardApi.Filters;

namespace StandardApi.Controllers.V1
{
    [ApiKeyAuth]
    public class SecretController : Controller
    {
        [HttpGet("secret")]
        public IActionResult GetSecret()
        {
            return Ok("No screts right now");
        }
    }
}
