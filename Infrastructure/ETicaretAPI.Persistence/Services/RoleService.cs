using ETicaretAPI.Application.Abstractions.Services;
using ETicaretAPI.Application.DTOs.Role;
using ETicaretAPI.Domain.Entities.Identity;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaretAPI.Persistence.Services
{
    public class RoleService : IRoleService
    {
        readonly RoleManager<AppRole> roleManager;

        public RoleService(RoleManager<AppRole> roleManager)
        {
            this.roleManager = roleManager;
        }
       public async Task<(List<GetRoleDTO>, int totalCount)> GetAllRolesAsync(int page, int size)
        {
            List<GetRoleDTO> roles = await roleManager.Roles.Select(r => new GetRoleDTO { Id = r.Id, Name = r.Name }).ToListAsync();
            if(page==-1 && size==-1)
            {
                page = 0;size = roles.Count;
            }
            var result = roles.Skip(page * size).Take(size).ToList();
            return (result, roles.Count);
        }

        public async Task<GetRoleDTO> GetRoleByIdAsync(string id)
        {
            var role = await roleManager.FindByIdAsync(id);
            return new() { Id = role.Id, Name = role.Name };
        }

        public async Task<bool> CreateRoleAsync(string name)
        {
            var result = await roleManager.CreateAsync(new AppRole() { Id=Guid.NewGuid().ToString(), Name = name });
            return result.Succeeded;
        }

        public async Task<bool> DeleteRoleAsync(string name)
        {
            var role = await roleManager.FindByNameAsync(name);
            var result= await roleManager.DeleteAsync(role);
            return result.Succeeded;
        }

       
        public async Task<bool> UpdateRoleAsync(string id,string name)
        {
            var role = await roleManager.FindByIdAsync(id);
            role.Name = name;
            var result = await roleManager.UpdateAsync(role);
            return result.Succeeded;
        }

    
    }
}
