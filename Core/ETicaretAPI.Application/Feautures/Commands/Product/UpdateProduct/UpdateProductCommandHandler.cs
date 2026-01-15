using ETicaretAPI.Application.Abstractions.Services;
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

        readonly IProductService productService;

        public UpdateProductCommandHandler(IProductService productService)
        {
            this.productService = productService;
        }

        public async Task<UpdateroductCommandResponse> Handle(UpdateProductCommanRequest request, CancellationToken cancellationToken)
        {
            await productService.UpdateAsync(new DTOs.Product.UpdateProductDTO()
            {
                Id = request.Id,
                Name = request.Name,
                Price = request.Price,
                Categories = request.Categories,
                Stock = request.Stock
            });
            return new();
        }
    }
}
