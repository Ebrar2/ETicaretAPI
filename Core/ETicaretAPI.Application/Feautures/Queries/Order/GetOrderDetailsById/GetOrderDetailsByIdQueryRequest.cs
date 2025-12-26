using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaretAPI.Application.Feautures.Queries.Order.GetOrderDetailsById
{
    public  class GetOrderDetailsByIdQueryRequest:IRequest<GetOrderDetailsByIdQueryResponse>
    {
        public string Id { get; set; }
    }
}
