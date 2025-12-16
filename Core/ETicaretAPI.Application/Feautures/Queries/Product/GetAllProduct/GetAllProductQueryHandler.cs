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
        readonly IProductReadRepository productReadRepository;
        readonly IConfiguration configuration;

        public GetAllProductQueryHandler(IProductReadRepository productReadRepository,IConfiguration configuration)
        {
            this.productReadRepository = productReadRepository;
            this.configuration = configuration;
        }

        public async Task<GetAllProductQueryResponse> Handle(GetAllProductQueryRequest request, CancellationToken cancellationToken)
        {
            var totalCount = productReadRepository.GetAll(false).Count();
           var products = productReadRepository.GetAll(false).Skip(request.Page * request.Size).Take(request.Size).Include(p=>p.ProductImageFiles).Select(p=> new
            {
                p.Id,
                p.Name,
                p.Price,
                p.Stock,
                p.UpdatedDate,
                p.CreatedDate,
                p.ProductImageFiles
            }).ToList();
            
           
            return new GetAllProductQueryResponse()
            {
                totalCount = totalCount,
                Products = products,
                BaseUrl= configuration["BaseStorageUrl"]
            }
            ;
          
        }
    }
}
