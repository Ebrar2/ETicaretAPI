using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaretAPI.Application.DTOs.Order
{
    public class GetOrderDetailsDTO
    {
        public string Id { get; set; }
        public string Address { get; set; }
        public string Description { get; set; }
        public float TotalPrice { get; set; }
        public List<OrderBasketItem> BasketItems { get; set; }
    }
    public class OrderBasketItem
    {
        public string ProductName { get; set; }
        public float ProductPrice { get; set; }
        public int Quantity { get; set; }
    }
}
