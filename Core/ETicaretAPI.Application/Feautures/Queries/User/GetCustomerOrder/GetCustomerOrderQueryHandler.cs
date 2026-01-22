using ETicaretAPI.Application.Abstractions.Services;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaretAPI.Application.Feautures.Queries.User.GetCustomerOrder
{
    public class GetCustomerOrderQueryHandler : IRequestHandler<GetCustomerOrderQueryRequest, GetCustomerOrderQueryResponse>
    {
        readonly IUserService userService;

        public GetCustomerOrderQueryHandler(IUserService userService)
        {
            this.userService = userService;
        }

        public async Task<GetCustomerOrderQueryResponse> Handle(GetCustomerOrderQueryRequest request, CancellationToken cancellationToken)
        {
            var orders = await userService.GetCustomerOrdersAsync(request.Id);
            return new()
            {CustomerOrders = orders
            };
        }
    }
}
