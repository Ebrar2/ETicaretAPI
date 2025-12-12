using ETicaretAPI.Application.Abstractions.Services;
using ETicaretAPI.Application.Abstractions.Token;
using ETicaretAPI.Application.DTOs;
using ETicaretAPI.Application.Exceptions.User;
using ETicaretAPI.Domain.Entities.Identity;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaretAPI.Application.Feautures.Commands.User.LoginUser
{
    public class LoginUserCommandHandler : IRequestHandler<LoginUserCommandRequest, LoginUserCommandResponse>
    {
        readonly IAuthService authService;

        public LoginUserCommandHandler(IAuthService authService)
        {
            this.authService = authService;
        }

        public async Task<LoginUserCommandResponse> Handle(LoginUserCommandRequest request, CancellationToken cancellationToken)
        {

          var result= await  authService.LoginAsync(new DTOs.User.LoginDTO() {UsernameOrEmail=request.UsernameOrEmail, Password = request.Password });
            if (result.Succeeded)
                return new LoginUserSuccessCommandResponse() { Succeeded = result.Succeeded, AccessToken = result.AccessToken, Message = result.Message,RefreshToken=result.RefreshToken };
            return new LoginUserErrorCommandResponse() { Message = result.Message };
        }
    }
}
