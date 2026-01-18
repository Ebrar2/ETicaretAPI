using ETicaretAPI.Application.Abstractions.Services;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaretAPI.Application.Feautures.Queries.User.GetAllUsers
{
    public class GetAllUsersQueryHandler : IRequestHandler<GetAllUsersQueryRequest, GetAllUsersQueryResponse>
    {
        readonly IUserService userService;

        public GetAllUsersQueryHandler(IUserService userService)
        {
            this.userService = userService;
        }

        public async Task<GetAllUsersQueryResponse> Handle(GetAllUsersQueryRequest request, CancellationToken cancellationToken)
        {
           var (users,totalCount)= await userService.GetAllUsersAsync(request.Page, request.Size,request.Name);
            return new GetAllUsersQueryResponse() { Users = users, TotatlCount = totalCount };
        }
    }
}
