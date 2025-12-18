using ETicaretAPI.Application.Abstractions.Services;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaretAPI.Application.Feautures.Commands.Basket.AddItemToBasketItem
{
    public class AddItemToBasketItemCommandHandler : IRequestHandler<AddItemToBasketItemCommandRequest, AddItemToBasketItemCommandResponse>
    {
        readonly IBasketService basketService;

        public AddItemToBasketItemCommandHandler(IBasketService basketService)
        {
            this.basketService = basketService;
        }

        public async Task<AddItemToBasketItemCommandResponse> Handle(AddItemToBasketItemCommandRequest request, CancellationToken cancellationToken)
        {
            await basketService.AddItemAsync(new ViewModels.Baskets.CreateBasketItemVM()
            {
                ProductId=request.ProductId,
                Quantity=request.Quantity
            });
            return new();
        }
    }
}
