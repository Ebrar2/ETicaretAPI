using ETicaretAPI.Application.Abstractions.Services;
using ETicaretAPI.Application.DTOs.Order;
using ETicaretAPI.Application.DTOs.Product;
using ETicaretAPI.Application.Helpers;
using ETicaretAPI.Application.Repositories;
using ETicaretAPI.Domain.Entities;
using ETicaretAPI.Domain.Entities.Identity;
using ETicaretAPI.Persistence.Repositories;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaretAPI.Persistence.Services
{
    public class OrderService : IOrderService
    {
        readonly IOrderWriteRepository orderWriteRepository;
        readonly IOrderReadRepository orderReadRepository;
        readonly IHttpContextAccessor httpContextAccessor;
        readonly UserManager<AppUser> userManager;
        readonly IMailService mailService;
        readonly IConfiguration configuration;
        public OrderService(IOrderWriteRepository orderWriteRepository, IOrderReadRepository orderReadRepository, IHttpContextAccessor httpContextAccessor, UserManager<AppUser> userManager, IMailService mailService,IConfiguration configuration)
        {
            this.orderWriteRepository = orderWriteRepository;
            this.orderReadRepository = orderReadRepository;
            this.httpContextAccessor = httpContextAccessor;
            this.userManager = userManager;
            this.mailService = mailService;
            this.configuration = configuration;
        }

        private async Task<Basket> GetContextUserBasket()
        {
            var userName = httpContextAccessor.HttpContext.User.Identity.Name;
            if (!string.IsNullOrEmpty(userName))
            {

                var user = await userManager.Users.Include(u => u.Baskets).FirstOrDefaultAsync(u => u.UserName == userName);
                Basket activeBasket = user.Baskets.FirstOrDefault(b => b.IsOrdered == false);
                if (activeBasket != null)
                {
                    return activeBasket;
                }
                else
                {

                    return null;
                }
            }
            return null;
        }
        public async Task CreateOrderAsync(CreateOrderDTO createOrder)
        {
            var basket =await GetContextUserBasket();
            if (basket!=null)
            {
                basket.IsOrdered = true;
                basket.TotalPrice = createOrder.TotalPrice;
                await orderWriteRepository.AddAsync(new Domain.Entities.Order()
                {
                    Basket=basket,
                    Address = createOrder.Address,
                    Description = createOrder.Description
                });
                await orderWriteRepository.SaveAsync();
            }
        }
        public async Task<(List<GetOrderDTO>, int totalCount)> GetOrderAsync(int page, int size, string? orderCode)
        {
            var orders = await orderReadRepository.Table.Include(o => o.Basket).ThenInclude(b => b.User).ToListAsync();
            if (orderCode != null && orderCode.Length != 0 && orderCode != "null")
            {
                orders = orders.Select(o => new
                {
                    Orders = o,
                    Score = o.OrderCode.ToString().Similarity(orderCode)
                })
                .Where(x => x.Score > 0.9)
                .OrderByDescending(x => x.Score)
                .Select(x => x.Orders)
                .ToList();
            }
            List<GetOrderDTO> getOrderDTOs = new List<GetOrderDTO>();

            foreach(var order in orders)
            {
                GetOrderDTO getOrderDTO = new()
                {
                    Id = order.Id.ToString(),
                    OrderingUserName = order.Basket.User.NameSurname,
                    OrderCode = order.OrderCode,
                    CreatedDate = order.CreatedDate,
                    TotalPrice = order.Basket.TotalPrice,
                    IsCompleted=order.IsCompleted

                };
                getOrderDTOs.Add(getOrderDTO);
            }
            getOrderDTOs=getOrderDTOs.OrderByDescending(o => o.CreatedDate).ToList();
            return (getOrderDTOs.Skip(page * size).Take(size).ToList(), getOrderDTOs.Count);
        }

        public async Task<GetOrderDetailsDTO> GetOrdertDetails(string orderId)
        {
            var order = await orderReadRepository.Table.Include(o => o.Basket).ThenInclude(b => b.BasketItems).ThenInclude(b => b.Product).FirstOrDefaultAsync(o => o.Id == Guid.Parse(orderId));
            List<OrderBasketItem> orderBasketItems = new();
            foreach(var basketItem in order.Basket.BasketItems)
            {
                OrderBasketItem orderBasketItem = new();
                orderBasketItem.ProductPrice = basketItem.Product.Price;
                orderBasketItem.ProductName = basketItem.Product.Name;
                orderBasketItem.Quantity = basketItem.Quantity;
                orderBasketItems.Add(orderBasketItem);
            }
            return new()
            {
                Id = order.Id.ToString(),
                Address = order.Address,
                BasketItems = orderBasketItems,
                Description = order.Description,
                TotalPrice = order.Basket.TotalPrice,
                IsCompleted = order.IsCompleted
            };
        }

        public async Task CompleteOrderAsync(string orderId)
        {
            var order = await orderReadRepository.Table.Include(o=>o.Basket).ThenInclude(b=>b.User).FirstOrDefaultAsync(o=>o.Id==Guid.Parse(orderId));
            if(order!=null)
            {
                order.IsCompleted = true;
                int result=await orderWriteRepository.SaveAsync();
                if(result>0)
                    await mailService.SendOrderCompletedMailAsync(order.Basket.User.Email,order.Basket.User.NameSurname, order.OrderCode.ToString());
            }
        }

        public async Task<List<GetDashboardDatas>> GetDashboardDatasAsync(int month)
        {
            DateTime now = DateTime.UtcNow;
            int specialRange = month;
            DateTime beforeFiveMonths = now.AddMonths(specialRange*-1);
            List<GetDashboardDatas> datas = new List<GetDashboardDatas>();
            var orders = await orderReadRepository.Table.Include(o=>o.Basket).ThenInclude(b=>b.BasketItems).Where(o => o.CreatedDate > beforeFiveMonths).OrderBy(o => o.CreatedDate).ToListAsync();

            for(int i=0;i<specialRange;i++)
            {
                var date = now.AddMonths(-1 * i);
                var dateOrders=orders.Where(o => o.CreatedDate.Month == date.Month).ToList();
                int totalProductCount = 0;
                float revenue = 0;
                revenue += dateOrders.Sum(o => o.Basket.TotalPrice);
                var total = dateOrders.Select(o => new
                {
                    total= o.Basket.BasketItems.Sum(b => b.Quantity)
                }).Select(o=>o.total);
                totalProductCount = total.Sum(t => t);
                string montString = date.ToString("MMMM", new CultureInfo(configuration["Culture"]));
                GetDashboardDatas getDashboardDatas = new GetDashboardDatas();
                getDashboardDatas.TotalProductCount = totalProductCount;
                getDashboardDatas.Revenue = Math.Round(revenue,2);
                getDashboardDatas.Month = montString;
                datas.Add(getDashboardDatas);
            }
            return datas;

        }
    }
}
