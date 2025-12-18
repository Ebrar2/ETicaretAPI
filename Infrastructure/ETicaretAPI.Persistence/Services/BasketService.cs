using ETicaretAPI.Application.Abstractions.Services;
using ETicaretAPI.Application.Repositories;
using ETicaretAPI.Application.ViewModels.Baskets;
using ETicaretAPI.Domain.Entities;
using ETicaretAPI.Domain.Entities.Identity;
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
    public class BasketService : IBasketService
    {
        readonly IHttpContextAccessor httpContextAccessor;
        readonly UserManager<AppUser> userManager;
        readonly IBasketItemWriteRepository basketItemWriteRepository;
        readonly IBasketWriteRepository basketWriteRepository;
        readonly IBasketItemReadRepository basketItemReadRepository;

        public BasketService(IHttpContextAccessor httpContextAccessor, UserManager<AppUser> userManager, IBasketItemWriteRepository basketItemWriteRepository, IBasketWriteRepository basketWriteRepository, IBasketItemReadRepository basketItemReadRepository)
        {
            this.httpContextAccessor = httpContextAccessor;
            this.userManager = userManager;
            this.basketItemWriteRepository = basketItemWriteRepository;
            this.basketWriteRepository = basketWriteRepository;
            this.basketItemReadRepository = basketItemReadRepository;
        }

        private async Task<Basket> GetContextUserBasket()
        {
           var userName= httpContextAccessor.HttpContext.User.Identity.Name;
            if(!string.IsNullOrEmpty(userName))
            {

                var user=await userManager.Users.Include(u=>u.Baskets).FirstOrDefaultAsync(u => u.UserName == userName);
                Basket activeBasket = user.Baskets.FirstOrDefault(b => b.IsOrdered == false);
                if(activeBasket!=null)
                {
                    return activeBasket;
                }
                else
                {
                    Basket newBasket = new Basket();
                    user.Baskets.Add(newBasket);
                    await basketWriteRepository.SaveAsync();
                    return newBasket;
                }
            }
            return null;
        }
        public async Task AddItemAsync(CreateBasketItemVM createBasketItemVM)
        {
            var basket = await GetContextUserBasket();
            if(basket!=null)
            {
                var basketItem = await basketItemReadRepository.GetSingleAsync(b =>b.BasketId==basket.Id && b.ProductId==Guid.Parse(createBasketItemVM.ProductId));
                if (basketItem != null)
                    basketItem.Quantity++;
                else
                {
                   await basketItemWriteRepository.AddAsync(new BasketItem() { BasketId = basket.Id, ProductId = Guid.Parse(createBasketItemVM.ProductId), Quantity = createBasketItemVM.Quantity });
                }
                await  basketItemWriteRepository.SaveAsync();
            }
        }

        public async Task<List<BasketItem>> GetBasketItemsAsync()
        {
            var basket =await GetContextUserBasket();
            var basketItems = await basketItemReadRepository.Table.Include(b=>b.Product).Where(b => b.BasketId == basket.Id).ToListAsync();

            return basketItems;
        }

        public async Task RemoveBasketItemAsync(string basketItemId)
        {
            var basketItem = await basketItemReadRepository.GetByIdAsync(basketItemId);
            if(basketItem!=null)
            {
                basketItemWriteRepository.Remove(basketItem);
                await basketItemWriteRepository.SaveAsync();
            }
        }

        public async Task UpdateQuantityAsync(UpdateBasketItemVM updateBasketItemVM)
        {
            var basketItem = await basketItemReadRepository.GetByIdAsync(updateBasketItemVM.BasketItemId);
           if(basketItem!=null)
            {
                basketItem.Quantity = updateBasketItemVM.Quantity;
               await basketItemWriteRepository.SaveAsync();
            }
        }
    }
}
