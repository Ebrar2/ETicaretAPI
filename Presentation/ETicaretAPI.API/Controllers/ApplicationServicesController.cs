
using ETicaretAPI.Application.Abstractions.Services.Configurations;
using Microsoft.AspNetCore.Mvc;


namespace ETicaretAPI.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApplicationServicesController : ControllerBase
    {
        readonly IService service;

        public ApplicationServicesController(IService service)
        {
            this.service = service;
        }
        [HttpGet]
        public IActionResult GetAuthorizedDefinitionEndpoints()
        {
            var datas = service.GetAuthorizedDefinitionEndpoints(typeof(Program));
            return Ok(datas);
        }
    }
}
