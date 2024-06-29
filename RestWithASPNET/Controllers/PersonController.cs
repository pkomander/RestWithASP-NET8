using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace RestWithASPNET.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonController : ControllerBase
    {

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok();
        }
    }
}
