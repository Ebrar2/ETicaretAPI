using ETicaretAPI.Application.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaretAPI.Application.Feautures.Commands.ProductImageFile.ChangeShowcaseProductImage
{
    public class ChangeShowcaseForProductImageCommandHandler : IRequestHandler<ChangeShowcaseForProductImageCommandRequest, ChangeShowcaseForProductImageCommandResponse>
    {
        readonly IProductImageFileWriteRepository productImageFileWriteRepository;

        public ChangeShowcaseForProductImageCommandHandler(IProductImageFileWriteRepository productImageFileWriteRepository)
        {
            this.productImageFileWriteRepository = productImageFileWriteRepository;
        }

        public async Task<ChangeShowcaseForProductImageCommandResponse> Handle(ChangeShowcaseForProductImageCommandRequest request, CancellationToken cancellationToken)
        {
            var mainQuery = productImageFileWriteRepository.Table.Include(p => p.Products)
                       .SelectMany(p => p.Products, (pImage, product) => new
                       {
                           pImage,
                           product
                       });
            var getMainProductImage = await mainQuery.FirstOrDefaultAsync(p => p.product.Id == Guid.Parse(request.ProductId) && p.pImage.Showcase);
            if (getMainProductImage != null)
                getMainProductImage.pImage.Showcase = false;
            var getNewMainProductImage = await mainQuery.FirstOrDefaultAsync(p => p.pImage.Id == Guid.Parse(request.ImageId));
           if(getNewMainProductImage!=null)
             getNewMainProductImage.pImage.Showcase = true;
            await productImageFileWriteRepository.SaveAsync();


             return new();       
        }
    }
}
