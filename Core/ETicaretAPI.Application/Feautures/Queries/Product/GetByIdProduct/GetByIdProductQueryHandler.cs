using ETicaretAPI.Application.Abstractions.Services;
using ETicaretAPI.Application.Repositories;
using ETicaretAPI.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaretAPI.Application.Feautures.Queries.Product.GetByIdProduct
{
    public class GetByIdProductQueryHandler : IRequestHandler<GetByIdProductQueryRequest, GetByIdProductQueryResponse>
    {
        readonly IProductService productService;

        public GetByIdProductQueryHandler(IProductService productService)
        {
            this.productService = productService;
        }

        public async Task<GetByIdProductQueryResponse> Handle(GetByIdProductQueryRequest request, CancellationToken cancellationToken)
        {
           var result= await productService.GetProductByIdWithCategoriesAsync(request.Id);
            return new()
            {
                Name = result.Name,
                Price = result.Price,
                Stock = result.Stock,
                Categories = result.Categories
            };
        }
    }
}
