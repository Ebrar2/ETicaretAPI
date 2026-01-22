using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaretAPI.Application.Feautures.Queries.User.GetAllCustomers
{
   public  class GetAllCustomersQueryRequest:IRequest<GetAllCustomersQueryResponse>
    {
        public int Page { get; set; } = 0;
        public int Size { get; set; } = 5;
        public string? Name { get; set; }
    }
}
