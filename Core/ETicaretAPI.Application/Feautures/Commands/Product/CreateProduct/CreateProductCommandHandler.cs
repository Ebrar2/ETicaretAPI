using ETicaretAPI.Application.Abstractions.Hubs;
using ETicaretAPI.Application.Repositories;
using ETicaretAPI.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaretAPI.Application.Feautures.Commands.Product.CreateProduct
{
    public class CreateProductCommandHandler : IRequestHandler<CreateProductCommandRequest, CreateProductCommandResponse>
    {
        readonly IProductWriteRepository productWriteRepository;
        readonly IProductHubService productHubService;
        public CreateProductCommandHandler(IProductWriteRepository productWriteRepository,IProductHubService productHubService)
        {
            this.productHubService = productHubService;
            this.productWriteRepository = productWriteRepository;
        }

        public async Task<CreateProductCommandResponse> Handle(CreateProductCommandRequest request, CancellationToken cancellationToken)
        {
            await productWriteRepository.AddAsync(new Domain.Entities.Product()
            {
                Name = request.Name,
                Price = request.Price,
                Stock = request.Stock
            });
            await productWriteRepository.SaveAsync();
            await productHubService.ProductAddedAsync("ürün eklendi!!!");
            return new();
        }
    }
}
