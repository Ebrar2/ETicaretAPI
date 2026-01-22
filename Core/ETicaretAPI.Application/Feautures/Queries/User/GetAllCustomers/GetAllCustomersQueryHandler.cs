using ETicaretAPI.Application.Abstractions.Services;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaretAPI.Application.Feautures.Queries.User.GetAllCustomers
{
    public class GetAllCustomersQueryHandler : IRequestHandler<GetAllCustomersQueryRequest, GetAllCustomersQueryResponse>
    {
        readonly IUserService userService;

        public GetAllCustomersQueryHandler(IUserService userService)
        {
            this.userService = userService;
        }

        public async Task<GetAllCustomersQueryResponse> Handle(GetAllCustomersQueryRequest request, CancellationToken cancellationToken)
        {
            var (customers, totalCount) = await userService.GetAllCustomersAsync(request.Page, request.Size, request.Name);
            return new GetAllCustomersQueryResponse() { Customers = customers, TotalCount = totalCount };
        }
    }
}
