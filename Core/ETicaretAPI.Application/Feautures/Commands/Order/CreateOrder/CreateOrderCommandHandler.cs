using ETicaretAPI.Application.Abstractions.Hubs;
using ETicaretAPI.Application.Abstractions.Services;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaretAPI.Application.Feautures.Commands.Order.CreateOrder
{
    public class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommandRequest, CreateOrderCommandResponse>
    {
        readonly IOrderService orderService;
        readonly IOrderHubService orderHubService;

        public CreateOrderCommandHandler(IOrderService orderService, IOrderHubService orderHubService)
        {
            this.orderService = orderService;
            this.orderHubService = orderHubService;
        }

        public async Task<CreateOrderCommandResponse> Handle(CreateOrderCommandRequest request, CancellationToken cancellationToken)
        {
           await orderService.CreateOrderAsync(new DTOs.Order.CreateOrderDTO()
            {
                Description = request.Description,
                Address = request.Address,
                TotalPrice=request.TotalPrice
            });
           await orderHubService.OrderCreatedaAsync("Yeni sipariş :) !!!");
            return new();
        }
    }
}
