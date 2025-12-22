using ETicaretAPI.Application.Abstractions.Hubs;
using ETicaretAPI.SignalR.Hubs;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaretAPI.SignalR.HubServices
{
    public class OrderHubService : IOrderHubService
    {
        readonly IHubContext<OrderHub> context;

        public OrderHubService(IHubContext<OrderHub> context)
        {
            this.context = context;
        }

        public async Task OrderCreatedaAsync(string message)
        {
            await context.Clients.All.SendAsync(ReceiveFunctionNames.OrderCreatedMessage, message);
        }
    }
}
