using ETicaretAPI.Application.Abstractions.Services;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaretAPI.Application.Feautures.Queries.Product.GetProductDetails
{
    public class GetProductDetailsQueryHandler : IRequestHandler<GetProductDetailsQueryRequest, GetProductDetailsQueryResponse>
    {
        readonly IProductService productService;

        public GetProductDetailsQueryHandler(IProductService productService)
        {
            this.productService = productService;
        }

        public async Task<GetProductDetailsQueryResponse> Handle(GetProductDetailsQueryRequest request, CancellationToken cancellationToken)
        {
            var productDetails = await productService.GetProductDetailsAsync(request.Id);
            return new()
            {
                Name = productDetails.Name,
                Price = productDetails.Price,
                Stock = productDetails.Stock,
                Images = productDetails.Images
            };
        }
    }
}
