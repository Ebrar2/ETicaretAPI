using ETicaretAPI.Application.Abstractions.Storage;
using ETicaretAPI.Application.Repositories;
using ETicaretAPI.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaretAPI.Application.Feautures.Commands.ProductImageFile.UploadProductImage
{
    public class UploadProductImageCommandHandler : IRequestHandler<UploadProductImageCommandRequest, UploadProductImageCommandResponse>
    {
        readonly IProductImageFileWriteRepository productImageFileWriteRepository;
        readonly IProductReadRepository productReadRepository;

        public UploadProductImageCommandHandler(IProductImageFileWriteRepository productImageFileWriteRepository, IProductReadRepository productReadRepository)
        {
            this.productImageFileWriteRepository = productImageFileWriteRepository;
            this.productReadRepository = productReadRepository;
        }

        public async Task<UploadProductImageCommandResponse> Handle(UploadProductImageCommandRequest request, CancellationToken cancellationToken)
        {
            var product = await productReadRepository.GetByIdAsync(request.Id);
            await productImageFileWriteRepository.AddRangeAsync(request.Datas.Select(f => new ETicaretAPI.Domain.Entities.ProductImageFile()
            {
                FileName = f.fileName,
                Path = f.pathOrContainerName,
                Storage = request.StorageName,
                Products = new List<ETicaretAPI.Domain.Entities.Product>() { product }
            }).ToList());

            await productImageFileWriteRepository.SaveAsync();
            return new();
        }
    }
}
