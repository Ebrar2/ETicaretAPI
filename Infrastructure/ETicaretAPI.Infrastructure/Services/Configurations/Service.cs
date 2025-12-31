using ETicaretAPI.Application.Abstractions.Services.Configurations;
using ETicaretAPI.Application.Attributes;
using ETicaretAPI.Application.DTOs.Configuration;
using ETicaretAPI.Application.Enums;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ETicaretAPI.Infrastructure.Services.Configurations
{
    public class Service : IService
    {
        public List<MenuDTO> GetAuthorizedDefinitionEndpoints(Type type)
        {
            Assembly assembly = Assembly.GetAssembly(type);
            List<MenuDTO> menus = new List<MenuDTO>();
           var controllers= assembly.GetTypes().Where(type => type.IsAssignableTo(typeof(ControllerBase)));
            foreach(var controller in controllers)
            {
                var methods = controller.GetMethods().Where(m => m.IsDefined(typeof(AuthorizeDefinitionAttribute))).ToList();
                if(methods!=null && methods.Count!=0)
                {
                    foreach(var method in methods)
                    {
                        var attributes = method.GetCustomAttributes(true);
                        if(attributes!=null)
                        {
                            var attribute = attributes.FirstOrDefault(a => a.GetType() == typeof(AuthorizeDefinitionAttribute)) as AuthorizeDefinitionAttribute;
                            MenuDTO menu = menus.FirstOrDefault(m => m.Name == attribute.Menu);
                            if(menu==null)
                            {
                                menu = new MenuDTO() { Name = attribute.Menu };
                                menus.Add(menu);
                            }
                           var httpAttribute= attributes.FirstOrDefault(a => a.GetType().IsAssignableTo(typeof(HttpMethodAttribute))) as HttpMethodAttribute;
                            ActionDTO actionDTO = new ActionDTO() { ActionType =Enum.GetName(typeof(ActionTypes),attribute.ActionTypes), Definition = attribute.Definition};
                            if (httpAttribute != null)
                                actionDTO.HttpType = httpAttribute.HttpMethods.First();
                            else
                                actionDTO.HttpType = HttpMethods.Get;
                            actionDTO.Code = $"{actionDTO.HttpType}.{actionDTO.ActionType}.{actionDTO.Definition.Replace(" ", "")}";
                            menu.Actions.Add(actionDTO);
                            }
                    }
                }
            }
            return menus;
        }
    }
}
