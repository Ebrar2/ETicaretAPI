using ETicaretAPI.Application.Attributes;
using ETicaretAPI.Application.Consts;
using ETicaretAPI.Application.Enums;
using ETicaretAPI.Application.Feautures.Commands.AuthorizationEndpoint.AssignRoleEndpoints;
using ETicaretAPI.Application.Feautures.Queries.AuthorizationEndpoint.GetRolesToEndpoint;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace ETicaretAPI.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes ="Admin")]
    public class AuthorizationEndpointsController : ControllerBase
    {
        readonly IMediator mediator;

        public AuthorizationEndpointsController(IMediator mediator)
        {
            this.mediator = mediator;
        }
        [HttpGet]
        [AuthorizeDefinition(Menu = AuthorizeDefinitionConstants.AuthorizationEndpoints,Definition = "Get Roles To Endpoint",ActionTypes =ActionTypes.Reading)]
        public async Task<IActionResult> GetRolesToEndpoint([FromQuery]GetRolesToEndpointQueryRequest getRolesToEndpointQueryRequest)
        {
            return Ok(await mediator.Send(getRolesToEndpointQueryRequest));
        }
        [HttpPost]
        [AuthorizeDefinition(Menu = AuthorizeDefinitionConstants.AuthorizationEndpoints, Definition = "Assign Role Endpoints", ActionTypes = ActionTypes.Writing)]
        public async Task<IActionResult> AssignRoleEndpoints([FromBody]AssignRolesEndpointCommandRequest assignRolesEndpointCommand)
        {
            assignRolesEndpointCommand.Type = typeof(Program);
            return Ok(await mediator.Send(assignRolesEndpointCommand));
        }
    }
}
