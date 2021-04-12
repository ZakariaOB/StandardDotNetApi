using Microsoft.AspNetCore.Mvc;

namespace StandardApi.Controllers
{
    public class TestController : Controller
    {
        [HttpGet("api/user")]
        public IActionResult Get()
        {
            return Ok(new { Name = "ZAKARIA" });
        }
    }
}
