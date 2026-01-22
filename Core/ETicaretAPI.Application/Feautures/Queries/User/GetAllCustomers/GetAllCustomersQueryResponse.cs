using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaretAPI.Application.Feautures.Queries.User.GetAllCustomers
{
    public class GetAllCustomersQueryResponse
    {
        public object Customers { get; set; }
        public int TotalCount { get; set; }
    }
}
