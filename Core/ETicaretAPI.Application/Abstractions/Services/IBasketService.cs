using ETicaretAPI.Application.ViewModels.Baskets;
using ETicaretAPI.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaretAPI.Application.Abstractions.Services
{
    public interface IBasketService
    {
        Task<List<BasketItem>> GetBasketItemsAsync();
        Task AddItemAsync(CreateBasketItemVM createBasketItemVM);
        Task UpdateQuantityAsync(UpdateBasketItemVM updateBasketItemVM);
        Task RemoveBasketItemAsync(string basketItemId);

    }
}
