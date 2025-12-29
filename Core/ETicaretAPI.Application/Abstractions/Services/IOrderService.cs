using ETicaretAPI.Application.DTOs.Order;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaretAPI.Application.Abstractions.Services
{
    public interface IOrderService
    {
        Task CreateOrderAsync(CreateOrderDTO createOrder);
        Task<(List<GetOrderDTO>,int totalCount)> GetOrderAsync(int page,int size);
        Task<GetOrderDetailsDTO> GetOrdertDetails(string orderId);
        Task CompleteOrderAsync(string orderId);
    }
}
