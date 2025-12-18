using ETicaretAPI.Application.Abstractions.Services;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaretAPI.Application.Feautures.Commands.Basket.UpdateQuantity
{
    public class UpdateQuantityCommandHandler : IRequestHandler<UpdateQuantityCommandRequest, UpdateQuantityCommandResponse>
    {
        readonly IBasketService basketService;

        public UpdateQuantityCommandHandler(IBasketService basketService)
        {
            this.basketService = basketService;
        }

        public async Task<UpdateQuantityCommandResponse> Handle(UpdateQuantityCommandRequest request, CancellationToken cancellationToken)
        {
          await basketService.UpdateQuantityAsync(new ViewModels.Baskets.UpdateBasketItemVM()
            {
                BasketItemId=request.BasketItemId,
                Quantity=request.Quantity
            });
            return new();
        }
    }
}
