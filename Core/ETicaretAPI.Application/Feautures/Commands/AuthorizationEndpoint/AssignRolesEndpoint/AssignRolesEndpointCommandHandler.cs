using ETicaretAPI.Application.Abstractions.Services;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaretAPI.Application.Feautures.Commands.AuthorizationEndpoint.AssignRoleEndpoints
{
    public class AssignRolesEndpointCommandHandler : IRequestHandler<AssignRolesEndpointCommandRequest, AssignRolesEndpointCommandResponse>
    {
        readonly IAuthorizationEndpointService authorizationEndpointService;

        public AssignRolesEndpointCommandHandler(IAuthorizationEndpointService authorizationEndpointService)
        {
            this.authorizationEndpointService = authorizationEndpointService;
        }

        public async Task<AssignRolesEndpointCommandResponse> Handle(AssignRolesEndpointCommandRequest request, CancellationToken cancellationToken)
        {
            await authorizationEndpointService.AssignRolesEndpointAsync(request.Roles, request.Menu, request.Code, request.Type);
            return new();
        
        }
    }
}
