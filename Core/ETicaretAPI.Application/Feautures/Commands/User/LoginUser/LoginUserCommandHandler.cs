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
        readonly UserManager<AppUser> userManager;
        readonly SignInManager<AppUser> signInManager;
        readonly ITokenHandler tokenHandler;
        public LoginUserCommandHandler(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager,ITokenHandler tokenHandler)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.tokenHandler = tokenHandler;
        }

        public async Task<LoginUserCommandResponse> Handle(LoginUserCommandRequest request, CancellationToken cancellationToken)
        {
            AppUser user = await userManager.FindByNameAsync(request.UsernameOrEmail);
            if (user == null)
                user = await userManager.FindByEmailAsync(request.UsernameOrEmail);

            if (user == null)
                return new LoginUserErrorCommandResponse() { Message = "Kullanıcı Bulunamadı" };
           var result= await signInManager.CheckPasswordSignInAsync(user, request.Password, false);
            Token token = tokenHandler.CreateAccessToken(5);
            if(result.Succeeded)
                return new LoginUserSuccessCommandResponse(){ 
                Message="Kullanıcı girişi başarılı",
                AccessToken=token.AccessToken};

            return new LoginUserErrorCommandResponse() { 
            Message="Kullanıcı girişi başarısız"};

        }
    }
}
