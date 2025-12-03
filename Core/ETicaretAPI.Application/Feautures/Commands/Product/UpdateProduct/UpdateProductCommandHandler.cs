using ETicaretAPI.Application.Repositories;
using ETicaretAPI.Application.ViewModels.Products;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaretAPI.Application.Feautures.Commands.Product.UpdateProduct
{
    public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommanRequest, UpdateroductCommandResponse>
    {
        readonly IProductWriteRepository productWriteRepository;
        readonly IProductReadRepository productReadRepository;


        public UpdateProductCommandHandler(IProductWriteRepository productWriteRepository, IProductReadRepository productReadRepository)
        {
            this.productWriteRepository = productWriteRepository;
            this.productReadRepository = productReadRepository;
        }

        public async Task<UpdateroductCommandResponse> Handle(UpdateProductCommanRequest request, CancellationToken cancellationToken)
        {
            var product = await productReadRepository.GetByIdAsync(request.Id);
            product.Stock = request.Stock;
            product.Price = request.Price;
            product.Name = request.Name;
            await productWriteRepository.SaveAsync();
            return new();
        }
    }
}
