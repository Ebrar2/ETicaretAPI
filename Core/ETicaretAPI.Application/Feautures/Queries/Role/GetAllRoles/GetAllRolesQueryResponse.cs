using ETicaretAPI.Application.DTOs.Role;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaretAPI.Application.Feautures.Queries.Role.GetAllRoles
{
    public class GetAllRolesQueryResponse
    {
    
        public int TotalCount { get; set; }
        public object Roles { get; set; }
    }
}
