using ETicaretAPI.Application.Abstractions.Services;
using ETicaretAPI.Application.DTOs.User;
using ETicaretAPI.Application.Exceptions.User;
using ETicaretAPI.Application.Feautures.Commands.User.CreateUser;
using ETicaretAPI.Application.Helpers;
using ETicaretAPI.Application.Repositories;
using ETicaretAPI.Domain.Entities.Identity;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.EntityFrameworkCore;
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
        readonly RoleManager<AppRole> roleManager;
        readonly IEndpointReadRepository endpointReadRepository;
        public UserService(UserManager<AppUser> userManager,RoleManager<AppRole> roleManager,IEndpointReadRepository endpointReadRepository)
        {
            this.userManager = userManager;
            this.roleManager = roleManager;
            this.endpointReadRepository = endpointReadRepository;
        }

        public async Task AssignRoleToUserAsync(string id, string[] roles)
        {
            var user = await userManager.FindByIdAsync(id);
            if(user!=null)
            {
                var userRoles = await userManager.GetRolesAsync(user);
                await userManager.RemoveFromRolesAsync(user,userRoles);
                await userManager.AddToRolesAsync(user, roles);
            }
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

        public async Task<(List<ListUserDTO> users, int totalCount)> GetAllUsersAsync(int page, int size)
        {
            var users = await userManager.Users.Select(u=>new ListUserDTO()
            {
                Id=u.Id,
                Email=u.Email,
                NameSurname=u.NameSurname,
                Username=u.UserName,
                TwoFactorEnabled=u.TwoFactorEnabled
            }).ToListAsync();
            var listUser = users.Skip(page * size).Take(size).ToList();
            return (listUser, users.Count);
        }

        public async Task<List<string>> GetRolesToUserAsync(string id)
        {
            var user = await userManager.FindByIdAsync(id);
            if(user!=null)
            {
                var roles = await userManager.GetRolesAsync(user);
                if (roles.Count != 0)
                    return roles.ToList();
            }
            return new List<string>();
        }

        public async Task<bool> HasRolePermissionToEndpointAsync(string username,string code)
        {
            var user = await userManager.FindByNameAsync(username);
            List<string> roleNames = (await userManager.GetRolesAsync(user)).ToList();
            var endpoint=await endpointReadRepository.Table.Include(e => e.Roles).FirstOrDefaultAsync(e => e.Code == code);
            if (endpoint == null)
                return false;
            foreach (var roleName in roleNames)
            {
               bool result= endpoint.Roles.Any(r => r.Name == roleName);
                if (result)
                    return true;
            }
            return false;

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
