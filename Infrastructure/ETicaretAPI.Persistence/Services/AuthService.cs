using ETicaretAPI.Application.Abstractions.Services;
using ETicaretAPI.Application.Abstractions.Token;
using ETicaretAPI.Application.DTOs;
using ETicaretAPI.Application.DTOs.User;
using ETicaretAPI.Application.Feautures.Commands.User.LoginUser;
using ETicaretAPI.Application.Feautures.Commands.User.LoginWithGoogle;
using ETicaretAPI.Domain.Entities.Identity;
using Google.Apis.Auth;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Google.Apis.Auth.GoogleJsonWebSignature;

namespace ETicaretAPI.Persistence.Services
{
    public class AuthService : IAuthService
    {
        readonly IConfiguration configuration;
        readonly UserManager<AppUser> userManager;
        readonly ITokenHandler tokenHandler;
        readonly SignInManager<AppUser> signInManager;
        readonly IUserService userService;
        public AuthService(IConfiguration configuration, UserManager<AppUser> userManager, ITokenHandler tokenHandler, SignInManager<AppUser> signInManager,IUserService userService)
        {
            this.configuration = configuration;
            this.userManager = userManager;
            this.tokenHandler = tokenHandler;
            this.signInManager = signInManager;
            this.userService = userService;
        }

        public async Task<LoginWithGoogleResponseDTO> GoogleLoginAsync(LoginWithGoogleDTO loginWithGoogleDTO)
        {

            ValidationSettings settings = new ValidationSettings()
            {
                Audience = new List<string>()
             {
                configuration["GoogleClientId"]
             }
            };
            Payload payload = await ValidateAsync(loginWithGoogleDTO.IdToken, settings);
            UserLoginInfo userLoginInfo = new UserLoginInfo(loginWithGoogleDTO.Provider, payload.Subject, loginWithGoogleDTO.Provider);
            AppUser user = await userManager.FindByLoginAsync(userLoginInfo.LoginProvider, userLoginInfo.ProviderKey);
            bool result = user != null;

            if (user == null)
            {
                user = await userManager.FindByEmailAsync(payload.Email);
                result = true;
                if (user == null)
                {
                    user = new AppUser()
                    {
                        Id = Guid.NewGuid().ToString(),
                        Email = payload.Email,
                        UserName = payload.Email,
                        NameSurname = payload.Name
                    };
                    var userCreateResult = await userManager.CreateAsync(user);
                    result = userCreateResult.Succeeded;
                }
            }
            if (result)
            {
                await userManager.AddLoginAsync(user, userLoginInfo);
                var token = tokenHandler.CreateAccessToken(1,user);
                await  userService.UpdateRefreshToken(token.RefreshToken, token.Expiration, 1, user);
                return new LoginWithGoogleResponseDTO {Succeeded=true, Message = "Giriş başarılı", AccessToken = token.AccessToken,RefreshToken=token.RefreshToken };
            }
            return new LoginWithGoogleResponseDTO { Message = "Giriş başarısız" };
        }

        public async Task<LoginResponseDTO> LoginAsync(LoginDTO loginDTO)
        {
            AppUser user = await userManager.FindByNameAsync(loginDTO.UsernameOrEmail);
            if (user == null)
                user = await userManager.FindByEmailAsync(loginDTO.UsernameOrEmail);

            if (user == null)
                return new LoginResponseDTO() { Message = "Kullanıcı Bulunamadı" };
            var result = await signInManager.CheckPasswordSignInAsync(user, loginDTO.Password, false);
            Token token = tokenHandler.CreateAccessToken(1,user);
            await userService.UpdateRefreshToken(token.RefreshToken, token.Expiration, 1, user);
            if (result.Succeeded)
                return new()
                {
                    Succeeded = true,
                    Message = "Kullanıcı girişi başarılı",
                    AccessToken = token.AccessToken,
                    RefreshToken = token.RefreshToken
                }; 

            return new()
            {
                Message = "Kullanıcı girişi başarısız"
            };

        }

        public async Task<LoginWithRefreshTokenDTO> RefreshLoginAsync(string refreshToken)
        {
            var user =await userManager.Users.FirstOrDefaultAsync(user => user.RefreshToken == refreshToken);
            if (user != null && user.RefreshTokenDate > DateTime.UtcNow)
            {
                Token token = tokenHandler.CreateAccessToken(1,user);
                await userService.UpdateRefreshToken(token.RefreshToken, token.Expiration, 1, user);
                return new()
                {
                    AccessToken = token.AccessToken,
                    RefreshToken = token.RefreshToken,
                    Succeeded = true
                };
            }
            else
                return new() { Succeeded = false };
        }
    }
}
