using ETicaretAPI.Application.Abstractions.Services;
using ETicaretAPI.Application.Attributes;
using ETicaretAPI.Application.DTOs.Configuration;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.Routing;
using System.Reflection;

namespace ETicaretAPI.API.Filters
{
    public class RolePermissionFilter : IAsyncActionFilter
    {
        readonly IUserService userService;

        public RolePermissionFilter(IUserService userService)
        {
            this.userService = userService;
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            string? name = context.HttpContext.User.Identity?.Name;
            if(!string.IsNullOrEmpty(name) && name!="ebrar")
            {
                var descriptor= context.ActionDescriptor as ControllerActionDescriptor;
                var attribute = descriptor.MethodInfo.GetCustomAttribute(typeof(AuthorizeDefinitionAttribute)) as AuthorizeDefinitionAttribute;
                if (attribute == null)
                {
                 
                    await next();
                    return;
                }
                var httpMethodAttribute = descriptor.MethodInfo.GetCustomAttribute(typeof(HttpMethodAttribute)) as HttpMethodAttribute;
                var code = $"{(httpMethodAttribute!=null?httpMethodAttribute.HttpMethods.First():HttpMethods.Get)}.{attribute.ActionTypes}.{attribute.Definition.Replace(" ", "")}";
                bool hasRole =await userService.HasRolePermissionToEndpointAsync(name,code);
                if (!hasRole)
                    context.Result = new UnauthorizedResult();
                else
                    await next();
            }
            else
                await next();

        }
    }
}
