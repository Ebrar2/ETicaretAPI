using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaretAPI.Application.Feautures.Commands.Basket.AddItemToBasketItem
{
    public class AddItemToBasketItemCommandRequest:IRequest<AddItemToBasketItemCommandResponse>
    {
        public string ProductId { get; set; }
        public int Quantity { get; set; }
    }
}
