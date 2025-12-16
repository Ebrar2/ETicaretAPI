using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaretAPI.Application.Feautures.Commands.ProductImageFile.ChangeShowcaseProductImage
{
    public class ChangeShowcaseForProductImageCommandRequest:IRequest<ChangeShowcaseForProductImageCommandResponse>
    {
        public string ImageId { get; set; }
        public string ProductId { get; set; }
    }
}
