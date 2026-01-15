using ETicaretAPI.Application.Abstractions.Services;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaretAPI.Application.Feautures.Commands.Product.UpdateProductStock
{
    public class UpdateProductStockCommandHandler : IRequestHandler<UpdateProductStockCommandRequest, UpdateProductStockCommandResponse>
    {
        readonly IProductService productService;

        public UpdateProductStockCommandHandler(IProductService productService)
        {
            this.productService = productService;
        }

        public async Task<UpdateProductStockCommandResponse> Handle(UpdateProductStockCommandRequest request, CancellationToken cancellationToken)
        {
            await productService.ChangeProductStockAsync(request.Id, request.Stock);
            return new();

        }
    }
}
