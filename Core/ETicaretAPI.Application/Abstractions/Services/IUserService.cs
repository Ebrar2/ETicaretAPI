using ETicaretAPI.Application.DTOs.User;
using ETicaretAPI.Domain.Entities.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaretAPI.Application.Abstractions.Services
{
    public interface IUserService
    {
        Task<(List<ListUserDTO> users, int totalCount)> GetAllUsersAsync(int page,int size);
        Task<CreateUserResponseDTO> CreateUserAsync(CreateUserDTO createUserDTO);
        Task UpdateUserPasswordAsync(string userId,string resetToken,string newPassword);
        Task UpdateRefreshTokenAsync(string refreshToken,DateTime accessTokenDate,int refreshTokenDate,AppUser user);
        Task AssignRoleToUserAsync(string id, string[] roles);
        Task<List<string>> GetRolesToUserAsync(string id);
        Task<bool> HasRolePermissionToEndpointAsync(string username,string code);
    }
}
