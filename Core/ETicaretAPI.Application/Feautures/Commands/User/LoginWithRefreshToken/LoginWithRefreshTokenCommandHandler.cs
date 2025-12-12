using ETicaretAPI.Application.Abstractions.Services;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaretAPI.Application.Feautures.Commands.User.LoginWithRefreshToken
{
    public class LoginWithRefreshTokenCommandHandler : IRequestHandler<LoginWithRefreshTokenCommandRequest, LoginWithRefreshTokenCommandResponse>
    {
        readonly IAuthService authService;

        public LoginWithRefreshTokenCommandHandler(IAuthService authService)
        {
            this.authService = authService;
        }

        public async Task<LoginWithRefreshTokenCommandResponse> Handle(LoginWithRefreshTokenCommandRequest request, CancellationToken cancellationToken)
        {
           var result=await authService.RefreshLoginAsync(request.RefreshToken);
            if(result.Succeeded)
                return new LoginWithRefreshTokenCommandSuccessResponse() { Succeeded = result.Succeeded, Message = result.Message, AccessToken = result.AccessToken, RefreshToken = result.RefreshToken };
            return new LoginWithRefreshTokenCommandErrorResponse() { Succeeded=false,Message="Olumsuz"};
        }
    }
}
