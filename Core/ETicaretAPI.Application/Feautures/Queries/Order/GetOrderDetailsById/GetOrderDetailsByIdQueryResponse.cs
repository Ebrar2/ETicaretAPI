using ETicaretAPI.Application.DTOs.Order;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaretAPI.Application.Feautures.Queries.Order.GetOrderDetailsById
{
    public class GetOrderDetailsByIdQueryResponse
    {
        public string Id { get; set; }
        public string Address { get; set; }
        public string Description { get; set; }
        public float TotalPrice { get; set; }
        public bool IsCompleted { get; set; }

        public List<OrderBasketItem> BasketItems { get; set; }
    }
}
