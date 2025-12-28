using ETicaretAPI.Application.Abstractions.Services;
using ETicaretAPI.Application.DTOs.User;
using ETicaretAPI.Application.Exceptions.User;
using ETicaretAPI.Application.Feautures.Commands.User.CreateUser;
using ETicaretAPI.Application.Helpers;
using ETicaretAPI.Domain.Entities.Identity;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaretAPI.Persistence.Services
{
    public class UserService : IUserService
    {
        readonly UserManager<AppUser> userManager;

        public UserService(UserManager<AppUser> userManager)
        {
            this.userManager = userManager;
        }

        public async Task<CreateUserResponseDTO> CreateUserAsync(CreateUserDTO createUserDTO)
        {
            IdentityResult result = await userManager.CreateAsync(new AppUser()
            {
                Id = Guid.NewGuid().ToString(),
                UserName = createUserDTO.UserName,
                NameSurname = createUserDTO.NameSurname,
                Email = createUserDTO.Email,

            }, createUserDTO.Password);
            CreateUserResponseDTO response = new() { Succeeded = result.Succeeded };
            if (result.Succeeded)
                response.Message = "Kullanıcı başarıyla eklendi";
            else
            {
                foreach (var error in result.Errors)
                    response.Message += error.Code.ToString() + "-" + error.Description;
            }
            return response;
        }

        public async Task UpdateRefreshTokenAsync(string refreshToken, DateTime accessTokenDate, int refreshTokenDate, AppUser user)
        {
            if(user!=null)
            {
                user.RefreshToken = refreshToken;
                user.RefreshTokenDate = accessTokenDate.AddMinutes(refreshTokenDate);
                await userManager.UpdateAsync(user);
            }
            else
                throw new NotFoundUserException();
        }

        public async Task UpdateUserPasswordAsync(string userId,string resetToken, string newPassword)
        {
            var user = await userManager.FindByIdAsync(userId);
            if(user!=null)
            {
                resetToken= resetToken.UrlDecode();
                IdentityResult result= await  userManager.ResetPasswordAsync(user,resetToken ,newPassword);
                if(result.Succeeded)
                {
                    await userManager.UpdateSecurityStampAsync(user);
                    return;
                }
            
            }

            throw new PasswordChangeFailedException("Şifre değiştirme işlemi başarısız");
        
    }
    }
}
