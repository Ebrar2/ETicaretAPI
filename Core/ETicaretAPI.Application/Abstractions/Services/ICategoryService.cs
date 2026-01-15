using ETicaretAPI.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaretAPI.Application.Abstractions.Services
{
    public interface ICategoryService
    {
        Task<(List<GetAllCategoriesDTO>, int totalCount)> GetAllCategoriesAsync(int page, int size);
        Task CreateCategoryAsync(string name);
        Task DeleteCategoryAsync(string id);
        Task UpdateCategoryAsync(string id, string name);
    }
}
