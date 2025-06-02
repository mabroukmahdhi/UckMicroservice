using Microsoft.AspNetCore.Mvc;

namespace Talks.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class HomeController : ControllerBase
    {
        [HttpGet]
        public string Get() =>
            $"Yes, your api is working!";
    }
}