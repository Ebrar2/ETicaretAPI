
using ETicaretAPI.Application.Abstractions.Services.Configurations;
using ETicaretAPI.Application.Attributes;
using ETicaretAPI.Application.Consts;
using ETicaretAPI.Application.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace ETicaretAPI.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes ="Admin")]
    public class ApplicationServicesController : ControllerBase
    {
        readonly IService service;

        public ApplicationServicesController(IService service)
        {
            this.service = service;
        }
        [HttpGet]
        [AuthorizeDefinition(Menu =AuthorizeDefinitionConstants.ApplicationServices,Definition = "Get Authorized Definition Endpoints",ActionTypes =ActionTypes.Reading)]
        public IActionResult GetAuthorizedDefinitionEndpoints()
        {
            var datas = service.GetAuthorizedDefinitionEndpoints(typeof(Program));
            return Ok(datas);
        }
    }
}
