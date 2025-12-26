using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaretAPI.Application.DTOs.Order
{
    public class GetOrderDTO
    {
        public string Id { get; set; }
        public int OrderCode { get; set; }
        public string OrderingUserName { get; set; }
        public float TotalPrice { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
