using ETicaretAPI.Application.Abstractions.Services;
using ETicaretAPI.Application.Abstractions.Services.Configurations;
using ETicaretAPI.Application.DTOs.Role;
using ETicaretAPI.Application.Repositories;
using ETicaretAPI.Domain.Entities;
using ETicaretAPI.Domain.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaretAPI.Persistence.Services
{
    public class AuthorizationEndpointService : IAuthorizationEndpointService
    {
        readonly IEndpointWriteRepository endpointWriteRepository;
        readonly IEndpointReadRepository endpointReadRepository;
        readonly IService service;
        readonly IMenuReadRepository menuReadRepository;
        readonly IMenuWriteRepository menuWriteRepository;
        readonly RoleManager<AppRole> roleManager;

        public AuthorizationEndpointService(IEndpointWriteRepository endpointWriteRepository, IEndpointReadRepository endpointReadRepository, IService service, IMenuReadRepository menuReadRepository, IMenuWriteRepository menuWriteRepository, RoleManager<AppRole> roleManager)
        {
            this.endpointWriteRepository = endpointWriteRepository;
            this.endpointReadRepository = endpointReadRepository;
            this.service = service;
            this.menuReadRepository = menuReadRepository;
            this.menuWriteRepository = menuWriteRepository;
            this.roleManager = roleManager;
        }

        public async Task AssignRolesEndpointAsync(string[] roles,string menuName, string code,Type type)
        {
            Menu? menu = await menuReadRepository.Table.Include(m=>m.Endpoints).ThenInclude(e=>e.Roles).FirstOrDefaultAsync(m => m.Name == menuName);
            if(menu==null)
            {
                menu = new  Menu{Id=Guid.NewGuid(), Name = menuName };
                await menuWriteRepository.AddAsync(menu);
            }
            Endpoint? endpoint = menu.Endpoints?.FirstOrDefault(e => e.Code == code);
            List<AppRole> appRoles = new List<AppRole>();
            foreach (var role in roles)
            {
                AppRole appRole = await roleManager.FindByNameAsync(role);
                appRoles.Add(appRole);
            }
            if (endpoint == null)
            {
                var menus = service.GetAuthorizedDefinitionEndpoints(type);
                var action=menus.FirstOrDefault(m => m.Name == menuName)?.Actions.FirstOrDefault(a=>a.Code==code);
                endpoint = new Endpoint()
                {
                    Id = Guid.NewGuid(),
                    ActionType = action.ActionType,
                    Code = action.Code,
                    Definition = action.Definition,
                    HttpType = action.HttpType,
                    Menu = menu
                };
                await endpointWriteRepository.AddAsync(endpoint);
                await endpointWriteRepository.SaveAsync();
             }
           
            foreach (var endpointRole in endpoint.Roles)
                endpoint.Roles.Remove(endpointRole);
            foreach (var role in appRoles)
                endpoint.Roles.Add(role);
            
         
             await menuWriteRepository.SaveAsync();


        }

        public async Task<List<string>> GetRolesToEndpointAsync(string menu, string code)
        {
            var endpoint = await endpointReadRepository.Table.Include(e=>e.Roles).Include(e => e.Menu).FirstOrDefaultAsync(e => e.Code == code && e.Menu.Name == menu);
            List<string> getRoles = new List<string>();
            var roles= endpoint?.Roles.Select(r => r.Name).ToList();
            if (roles != null)
                getRoles = roles;
            return getRoles;
       
        }
    }
}
