using ETicaretAPI.Application.Abstractions.Services;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaretAPI.Application.Feautures.Queries.Order
{
    public class GetOrdersQueryHandler : IRequestHandler<GetOrdersQueryRequest,GetOrdersQueryResponse>
    {
        readonly IOrderService orderService;

        public GetOrdersQueryHandler(IOrderService orderService)
        {
            this.orderService = orderService;
        }

        public async Task<GetOrdersQueryResponse> Handle(GetOrdersQueryRequest request, CancellationToken cancellationToken)
        {
            var (orders,totalCount) = await orderService.GetOrderAsync(request.Page,request.Size,request.OrderCode);

            return new GetOrdersQueryResponse() { Orders =orders, TotalCount=totalCount};
        }
      
    }
}
