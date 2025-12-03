using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaretAPI.Application.Feautures.Commands.ProductImageFile.DeleteProductImage
{
    public  class DeleteProductImageCommandRequest:IRequest<DeleteProductImageCommandResponse>
    {
       public string ProductId { get; set; }
       public string ImageId { get; set; }
    }
}
