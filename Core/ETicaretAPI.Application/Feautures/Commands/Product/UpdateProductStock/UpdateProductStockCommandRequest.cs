using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaretAPI.Application.Feautures.Commands.Product.UpdateProductStock
{
    public class UpdateProductStockCommandRequest:IRequest<UpdateProductStockCommandResponse>
    {
        public string Id { get; set; }
        public int Stock { get; set; }
    }
}
