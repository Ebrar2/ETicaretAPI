using ETicaretAPI.Application.Abstractions.Token;
using ETicaretAPI.Application.DTOs;
using ETicaretAPI.Domain.Entities.Identity;
using Google.Apis.Auth;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Google.Apis.Auth.GoogleJsonWebSignature;

namespace ETicaretAPI.Application.Feautures.Commands.User.LoginWithGoogle
{
    public class LoginWithGoogleCommandHandler : IRequestHandler<LoginWithGoogleCommandRequest, LoginWithGoogleCommandResponse>
    {
        readonly UserManager<AppUser> userManager;
        readonly ITokenHandler tokenHandler;

        public LoginWithGoogleCommandHandler(UserManager<AppUser> userManager,ITokenHandler tokenHandler)
        {
            this.userManager = userManager;
            this.tokenHandler = tokenHandler;
        }

        public async Task<LoginWithGoogleCommandResponse> Handle(LoginWithGoogleCommandRequest request, CancellationToken cancellationToken)
        {
            ValidationSettings settings = new GoogleJsonWebSignature.ValidationSettings()
            {
                Audience = new List<string>()
             {
                 "319438112970-qvb0of57h0uuuqh33nh3v2fd0da1cjsc.apps.googleusercontent.com"
             }
            };
            Payload payload = await GoogleJsonWebSignature.ValidateAsync(request.IdToken, settings);
            UserLoginInfo userLoginInfo = new UserLoginInfo(request.Provider, payload.Subject, request.Provider);
            AppUser user =await userManager.FindByLoginAsync(userLoginInfo.LoginProvider, userLoginInfo.ProviderKey);
            bool result = user != null;

            if (user==null)
            {
                user = await userManager.FindByEmailAsync(payload.Email);
                if(user==null)
                {
                    user = new AppUser()
                    {
                        Id = Guid.NewGuid().ToString(),
                        Email = payload.Email,
                        UserName = payload.Email,
                        NameSurname=payload.Name
                    };
                    var userCreateResult= await  userManager.CreateAsync(user);
                    result = userCreateResult.Succeeded;
                }
            }
            if (result)
            {
                await userManager.AddLoginAsync(user, userLoginInfo);
                var token=tokenHandler.CreateAccessToken(5);
                return new LoginWithCommanSuccessResponse { Message = "Giriş başarılı" ,AccessToken=token.AccessToken};
            }
            else
                return new LoginWithCommanErrorResponse { Message = "Giriş başarısız" };

        }
    }
}
