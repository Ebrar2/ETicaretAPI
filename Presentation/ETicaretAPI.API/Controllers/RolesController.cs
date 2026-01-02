using ETicaretAPI.Application.Attributes;
using ETicaretAPI.Application.Consts;
using ETicaretAPI.Application.Enums;
using ETicaretAPI.Application.Feautures.Commands.Role.CreateRole;
using ETicaretAPI.Application.Feautures.Commands.Role.DeleteRole;
using ETicaretAPI.Application.Feautures.Commands.Role.UpdateRole;
using ETicaretAPI.Application.Feautures.Queries.Role.GetAllRoles;
using ETicaretAPI.Application.Feautures.Queries.Role.GetRoleById;
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
    public class RolesController : ControllerBase
    {
        readonly IMediator mediator;

        public RolesController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpGet("[action]")]
        [AuthorizeDefinition(Menu = AuthorizeDefinitionConstants.Roles, Definition = "Get All Roles", ActionTypes = ActionTypes.Reading)]
        public async Task<IActionResult> GetAllRoles([FromQuery]GetAllRolesQueryRequest getAllRolesQueryRequest)
        {
            return Ok(await mediator.Send(getAllRolesQueryRequest));
        }

        [HttpGet("[action]/{id}")]
        [AuthorizeDefinition(Menu = AuthorizeDefinitionConstants.Roles, Definition = "Get Role By Id", ActionTypes = ActionTypes.Reading)]
        public async Task<IActionResult> GetRoleById([FromRoute] GetRoleByIdQueryRequest getRoleByIdQueryRequest)
        {
            return Ok(await mediator.Send(getRoleByIdQueryRequest));
        }


        [HttpPost("[action]")]
        [AuthorizeDefinition(Menu = AuthorizeDefinitionConstants.Roles, Definition = "Create Role", ActionTypes = ActionTypes.Writing)]
        public async Task<IActionResult> CreateRole([FromBody]CreateRoleCommandRequest createRoleCommandRequest)
        {
            return Ok(await mediator.Send(createRoleCommandRequest));
        }
        [HttpPut("[action]")]
        [AuthorizeDefinition(Menu = AuthorizeDefinitionConstants.Roles, Definition = "Update Role", ActionTypes = ActionTypes.Updating)]
        public async Task<IActionResult> UpdateRole([FromBody] UpdateRoleCommandRequest updateRoleCommandRequest)
        {
            return Ok(await mediator.Send(updateRoleCommandRequest));
        }
        [HttpDelete("{name}")]
        [AuthorizeDefinition(Menu = AuthorizeDefinitionConstants.Roles, Definition = "Delete Role", ActionTypes = ActionTypes.Deleting)]
        public async Task<IActionResult> DeleteRole([FromRoute] DeleteRoleCommandRequest deleteRoleCommandRequest)
        {
            return Ok(await mediator.Send(deleteRoleCommandRequest));
        }
    }
}
