using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaretAPI.Application.DTOs.Order
{
    public class GetDashboardDatas
    {
        public string Month { get; set; }
        public double Revenue { get; set; }
        public int TotalProductCount { get; set; }
    }
}
