using Microsoft.AspNetCore.Mvc;

namespace Attendees.Controllers
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