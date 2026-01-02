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
        public Task<AssignRolesEndpointCommandResponse> Handle(AssignRolesEndpointCommandRequest request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
