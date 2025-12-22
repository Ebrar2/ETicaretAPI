using ETicaretAPI.Application.Abstractions.Services;
using ETicaretAPI.Application.DTOs.Order;
using ETicaretAPI.Application.Repositories;
using ETicaretAPI.Domain.Entities;
using ETicaretAPI.Domain.Entities.Identity;
using ETicaretAPI.Persistence.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaretAPI.Persistence.Services
{
    public class OrderService : IOrderService
    {
        readonly IOrderWriteRepository orderWriteRepository;
        readonly IHttpContextAccessor httpContextAccessor;
        readonly UserManager<AppUser> userManager;

        public OrderService(IOrderWriteRepository orderWriteRepository, IHttpContextAccessor httpContextAccessor, UserManager<AppUser> userManager)
        {
            this.orderWriteRepository = orderWriteRepository;
            this.httpContextAccessor = httpContextAccessor;
            this.userManager = userManager;
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
                await orderWriteRepository.AddAsync(new Domain.Entities.Order()
                {
                    Basket=basket,
                    Address = createOrder.Address,
                    Description = createOrder.Description
                });
                await orderWriteRepository.SaveAsync();
            }
        }
    }
}
