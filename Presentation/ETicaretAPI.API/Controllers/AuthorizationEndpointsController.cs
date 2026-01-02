using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ETicaretAPI.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorizationEndpointsController : ControllerBase
    {

        [HttpPost]
        public IActionResult AssignRoleEndpoints()
        {
            return Ok();
        }
    }
}
