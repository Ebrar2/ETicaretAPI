using ETicaretAPI.Application.Abstractions.Services;
using ETicaretAPI.Application.DTOs.Order;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace ETicaretAPI.Application.Feautures.Queries.Order.GetOrderDetailsById
{
    public class GetOrderDetailsByIdQueryHandler : IRequestHandler<GetOrderDetailsByIdQueryRequest, GetOrderDetailsByIdQueryResponse>
    {
        readonly IOrderService orderService;

        public GetOrderDetailsByIdQueryHandler(IOrderService orderService)
        {
            this.orderService = orderService;
        }

        public async Task<GetOrderDetailsByIdQueryResponse> Handle(GetOrderDetailsByIdQueryRequest request, CancellationToken cancellationToken)
        {
            var order = await orderService.GetOrdertDetails(request.Id);
            return new GetOrderDetailsByIdQueryResponse()
            {
                Id = order.Id,
                Address = order.Address,
                BasketItems = order.BasketItems,
                Description = order.Description,
                TotalPrice = order.TotalPrice

            };
        }
    }
}
