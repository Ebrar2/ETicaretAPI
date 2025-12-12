using ETicaretAPI.Application.Abstractions.Services;
using ETicaretAPI.Application.Abstractions.Token;
using ETicaretAPI.Application.DTOs;
using ETicaretAPI.Application.DTOs.User;
using ETicaretAPI.Domain.Entities.Identity;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaretAPI.Application.Feautures.Commands.User.LoginWithGoogle
{
    public class LoginWithGoogleCommandHandler : IRequestHandler<LoginWithGoogleCommandRequest, LoginWithGoogleCommandResponse>
    {
        readonly IAuthService authService;

        public LoginWithGoogleCommandHandler(IAuthService authService)
        {
            this.authService = authService;
        }

        public async Task<LoginWithGoogleCommandResponse> Handle(LoginWithGoogleCommandRequest request, CancellationToken cancellationToken)
        {
           var result=await  authService.GoogleLoginAsync(new DTOs.User.LoginWithGoogleDTO()
            {
                IdToken = request.IdToken,
                Provider = request.Provider
            });
            if (result.Succeeded)
                return new LoginWithCommanSuccessResponse() { Succeeded = result.Succeeded, Message = result.Message,AccessToken=result.AccessToken,RefreshToken=result.RefreshToken };
            return new LoginWithCommanErrorResponse() { Succeeded = result.Succeeded, Message = result.Message };
        }
    }
}
