using ETicaretAPI.Application.Feautures.Commands.User.CreateUser;
using ETicaretAPI.Application.Feautures.Commands.User.LoginUser;
using ETicaretAPI.Application.Feautures.Commands.User.LoginWithGoogle;
using MediatR;
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
     

        [HttpPost]
        public async Task<IActionResult> Post(CreateUserCommandRequest createUserCommandRequest)
        {
           
            return Ok(await mediator.Send(createUserCommandRequest));
        }
        
    }
}
