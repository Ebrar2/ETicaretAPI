using ETicaretAPI.Application.Attributes;
using ETicaretAPI.Application.Consts;
using ETicaretAPI.Application.Enums;
using ETicaretAPI.Application.Feautures.Commands.AuthorizationEndpoint.AssignRoleEndpoints;
using ETicaretAPI.Application.Feautures.Commands.User.AssignRoleToUser;
using ETicaretAPI.Application.Feautures.Commands.User.CreateUser;
using ETicaretAPI.Application.Feautures.Commands.User.LoginUser;
using ETicaretAPI.Application.Feautures.Commands.User.LoginWithGoogle;
using ETicaretAPI.Application.Feautures.Commands.User.UpdateUserPassword;
using ETicaretAPI.Application.Feautures.Queries.Role.GetRoleById;
using ETicaretAPI.Application.Feautures.Queries.User.GetAllUsers;
using ETicaretAPI.Application.Feautures.Queries.User.GetRolesToUser;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Threading.Tasks;

namespace ETicaretAPI.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        readonly IMediator mediator;

        public UsersController(IMediator mediator)
        {
            this.mediator = mediator;
        }
        [HttpGet]
        [Authorize(AuthenticationSchemes =("Admin"))]
        [AuthorizeDefinition(Menu =AuthorizeDefinitionConstants.Users,Definition ="Get All Users",ActionTypes =ActionTypes.Reading)]
        public async Task<IActionResult> GetAllUsers([FromQuery]GetAllUsersQueryRequest getAllUsersQueryRequest)
        {
            return Ok(await mediator.Send(getAllUsersQueryRequest));
        }
        [HttpPost]
        public async Task<IActionResult> Post(CreateUserCommandRequest createUserCommandRequest)
        {
           
            return Ok(await mediator.Send(createUserCommandRequest));
        }
        [HttpPost("[action]")]
        public async Task<IActionResult> UpdatePassword([FromBody]UpdateUserPasswordCommandRequest updateUserPasswordCommandRequest)
        {

            return Ok(await mediator.Send(updateUserPasswordCommandRequest));
        }
        [HttpPost("[action]")]
        [Authorize(AuthenticationSchemes = ("Admin"))]
        [AuthorizeDefinition(Menu = AuthorizeDefinitionConstants.Users, Definition = "Assign Role To User", ActionTypes = ActionTypes.Writing)]

        public async Task<IActionResult> AssignRoleToUser([FromBody]AssignRoleToUserCommandRequest assignRoleToUserCommandRequest)
        {
            return Ok(await mediator.Send(assignRoleToUserCommandRequest));
        }
        [HttpGet("[action]/{id}")]
        [Authorize(AuthenticationSchemes = ("Admin"))]
        [AuthorizeDefinition(Menu = AuthorizeDefinitionConstants.Users, Definition = "Get Roles To User", ActionTypes = ActionTypes.Reading)]

        public async Task<IActionResult> GetRolesToUser([FromRoute]GetRolesToUserQueryRequest getRolesToUserQueryRequest)
        {
            return Ok(await mediator.Send(getRolesToUserQueryRequest));
        }
    }
}
