using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaretAPI.Application.Feautures.Queries.Order.GetDashboardData
{
   public class GetDashboardDataQueryRequest:IRequest <List <GetDashboardDataQueryResponse>>
    {
        public int Month { get; set; } = 5;
    }
}
