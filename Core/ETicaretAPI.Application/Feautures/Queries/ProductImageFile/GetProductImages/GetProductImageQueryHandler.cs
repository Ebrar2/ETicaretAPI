using ETicaretAPI.Application.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaretAPI.Application.Feautures.Queries.ProductImageFile.GetProductImages
{
    public class GetProductImageQueryHandler : IRequestHandler<GetProductImagesQueryRequest,List<GetProductImagesQueryResponse>>
    {
        readonly IProductReadRepository productReadRepository;
        readonly IConfiguration configuration;
        public GetProductImageQueryHandler(IProductReadRepository productReadRepository,IConfiguration configuration)
        {
            this.productReadRepository = productReadRepository;
            this.configuration = configuration;
        }

        public async Task<List<GetProductImagesQueryResponse>> Handle(GetProductImagesQueryRequest request, CancellationToken cancellationToken)
        {
            ETicaretAPI.Domain.Entities.Product product = await productReadRepository.Table.Include(p => p.ProductImageFiles).FirstOrDefaultAsync(p => p.Id == Guid.Parse(request.Id));
           
           
            return product.ProductImageFiles.Select(p => new GetProductImagesQueryResponse
            {
                Path = $"{configuration["BaseStorageUrl"]}/{p.Path}",
                FileName=p.FileName,
                Id= p.Id
            }).ToList();
        }
    }
}
