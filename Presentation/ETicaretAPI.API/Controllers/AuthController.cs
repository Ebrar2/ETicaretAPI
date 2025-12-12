using ETicaretAPI.Application.Feautures.Commands.User.LoginUser;
using ETicaretAPI.Application.Feautures.Commands.User.LoginWithGoogle;
using ETicaretAPI.Application.Feautures.Commands.User.LoginWithRefreshToken;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ETicaretAPI.API.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : Controller
    {
        readonly IMediator mediator;

        public AuthController(IMediator mediator)
        {
            this.mediator = mediator;
        }
        [HttpPost("[action]")]
        public async Task<IActionResult> Login(LoginUserCommandRequest loginUserCommandRequest)
        {
            return Ok(await mediator.Send(loginUserCommandRequest));
        }
        [HttpPost("[action]")]
        public async Task<IActionResult> LoginWithRefreshToken(LoginWithRefreshTokenCommandRequest loginWithRefreshToken)
        {
            return Ok(await mediator.Send(loginWithRefreshToken));
        }
        [HttpPost("[action]")]
        public async Task<IActionResult> LoginWithGoogle(LoginWithGoogleCommandRequest loginWithGoogleCommandRequest)
        {
            return Ok(await mediator.Send(loginWithGoogleCommandRequest));
        }
    }
}
