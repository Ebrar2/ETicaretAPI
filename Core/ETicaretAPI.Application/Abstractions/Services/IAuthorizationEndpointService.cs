using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaretAPI.Application.Abstractions.Services
{
   public  interface IAuthorizationEndpointService
    {

        Task AssignRolesEndpointAsync(string[] roles,string menuName, string code,Type type);
    }
}
