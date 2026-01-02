using ETicaretAPI.Application.Abstractions.Services;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaretAPI.Application.Feautures.Queries.Role.GetAllRoles
{
    public class GetAllRolesQueryHandler : IRequestHandler<GetAllRolesQueryRequest, GetAllRolesQueryResponse>
    {
        readonly IRoleService roleService;

        public GetAllRolesQueryHandler(IRoleService roleService)
        {
            this.roleService = roleService;
        }

        public async Task<GetAllRolesQueryResponse> Handle(GetAllRolesQueryRequest request, CancellationToken cancellationToken)
        {
            var (roles,totalCount)= await roleService.GetAllRolesAsync(request.Page, request.Size);
            return new() { Roles=roles,TotalCount=totalCount};
        }
    }
}
