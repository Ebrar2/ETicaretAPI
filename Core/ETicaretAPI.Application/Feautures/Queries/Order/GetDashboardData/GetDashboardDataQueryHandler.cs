using ETicaretAPI.Application.Abstractions.Services;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaretAPI.Application.Feautures.Queries.Order.GetDashboardData
{
    public class GetDashboardDataQueryHandler : IRequestHandler<GetDashboardDataQueryRequest, List<GetDashboardDataQueryResponse>>
    {
        readonly IOrderService orderService;

        public GetDashboardDataQueryHandler(IOrderService orderService)
        {
            this.orderService = orderService;
        }

        public async Task<List<GetDashboardDataQueryResponse>> Handle(GetDashboardDataQueryRequest request, CancellationToken cancellationToken)
        {
            var datas = await orderService.GetDashboardDatasAsync(request.Month);
            return datas.Select(d=>new GetDashboardDataQueryResponse()
            {
                Month=d.Month,
                Revenue=d.Revenue,
                TotalProductCount=d.TotalProductCount
            }).ToList();
        }
    }
}
