using ETicaretAPI.Application.Abstractions.Services;
using ETicaretAPI.Application.Repositories;
using ETicaretAPI.Application.RequestParmeters;
using ETicaretAPI.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaretAPI.Application.Feautures.Queries.Product.GetAllProduct
{
    public class GetAllProductQueryHandler : IRequestHandler<GetAllProductQueryRequest, GetAllProductQueryResponse>
    {
        readonly IProductService productService;
        readonly IConfiguration configuration;

        public GetAllProductQueryHandler(IProductService productService, IConfiguration configuration)
        {
            this.productService = productService;
            this.configuration = configuration;
        }

        public async Task<GetAllProductQueryResponse> Handle(GetAllProductQueryRequest request, CancellationToken cancellationToken)
        {

            var result = await productService.GetAllAsync(new DTOs.Product.GetAllProductDTO()
            {
                Page = request.Page,
                Size = request.Size,
                FilterCategories = request.FilterCategories,
                MaxPrice = request.MaxPrice,
                Name=request.Name,
                IsAscending=request.IsAscending

            });
           
            return new GetAllProductQueryResponse()
            {
                totalCount = result.TotalCount,
                Products = result.Products,
                BaseUrl= configuration["BaseStorageUrl"]
            }
            ;
          
        }
    }
}
