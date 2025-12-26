using ETicaretAPI.Application.DTOs.Order;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaretAPI.Application.Feautures.Queries.Order
{
    public class GetOrdersQueryResponse
    {
        public List<GetOrderDTO> Orders { get; set; }
        public int TotalCount { get; set; }
    }
  
}
