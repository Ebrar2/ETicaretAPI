using ETicaretAPI.Application.DTOs.Role;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaretAPI.Application.Abstractions.Services
{
    public interface IRoleService
    {
        Task<(List<GetRoleDTO>,int totalCount)> GetAllRolesAsync(int page,int size);
        Task<GetRoleDTO> GetRoleByIdAsync(string id);
        Task<bool> CreateRoleAsync(string name);
        Task<bool> DeleteRoleAsync(string name);
        Task<bool> UpdateRoleAsync(string id,string name);
    }
}
