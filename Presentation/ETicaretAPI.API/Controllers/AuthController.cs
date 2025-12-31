using ETicaretAPI.Application.Feautures.Commands.User.LoginUser;
using ETicaretAPI.Application.Feautures.Commands.User.LoginWithGoogle;
using ETicaretAPI.Application.Feautures.Commands.User.LoginWithRefreshToken;
using ETicaretAPI.Application.Feautures.Commands.User.ResetPassword;
using ETicaretAPI.Application.Feautures.Commands.User.VerifyResetToken;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ETicaretAPI.API.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
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
        [HttpPost("[action]")]
        public async Task<IActionResult> ResetPassword([FromBody]ResetPasswordComandRequest resetPasswordComandRequest)
        {
            return Ok(await mediator.Send(resetPasswordComandRequest));
        }
        [HttpPost("[action]")]
        public async Task<IActionResult> VerifyResetToken([FromBody] VerifyResetTokenCommandRequest verifyResetTokenCommand)
        {
            return Ok(await mediator.Send(verifyResetTokenCommand));
        }
    }
}
