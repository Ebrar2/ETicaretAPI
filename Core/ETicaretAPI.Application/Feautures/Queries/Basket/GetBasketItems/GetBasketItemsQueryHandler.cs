using ETicaretAPI.Application.Abstractions.Services;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaretAPI.Application.Feautures.Queries.Basket.GetBasketItems
{
    public class GetBasketItemsQueryHandler : IRequestHandler<GetBasketItemsQueryRequest, List<GetBasketItemsQueryResponse>>
    {
        readonly IBasketService basketService;

        public GetBasketItemsQueryHandler(IBasketService basketService)
        {
            this.basketService = basketService;
        }

        public async Task<List<GetBasketItemsQueryResponse>> Handle(GetBasketItemsQueryRequest request, CancellationToken cancellationToken)
        {
            var result = await basketService.GetBasketItemsAsync();
            return result.Select(b=>new GetBasketItemsQueryResponse()
            {
                BasketItemId=b.Id.ToString(),
                Name=b.Product.Name,
                Price=b.Product.Price,
                Quantity=b.Quantity
            }).ToList();
        }
    }
}
