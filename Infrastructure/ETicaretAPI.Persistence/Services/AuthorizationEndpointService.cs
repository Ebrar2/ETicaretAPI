using ETicaretAPI.Application.Abstractions.Services;
using ETicaretAPI.Application.Abstractions.Services.Configurations;
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
            Menu? menu = await menuReadRepository.Table.Include(m=>m.Endpoints).FirstOrDefaultAsync(m => m.Name == menuName);
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
               
                menu.Endpoints.Add(new Endpoint()
                {
                    Id=Guid.NewGuid(),
                    ActionType=action.ActionType,
                    Code=action.Code,
                    Definition=action.Definition,
                    HttpType=action.HttpType,
                    Roles=appRoles
                });
             }
            await menuWriteRepository.SaveAsync();
        }
    }
}
