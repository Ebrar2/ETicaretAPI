using ETicaretAPI.Application.Abstractions.Services;
using ETicaretAPI.Application.DTOs.Category;
using ETicaretAPI.Application.Repositories;
using ETicaretAPI.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaretAPI.Persistence.Services
{
    public class CategoryService : ICategoryService
    {
        readonly ICategoryReadRepository categoryReadRepository;
        readonly ICategoryWriteRepository categoryWriteRepository;

        public CategoryService(ICategoryReadRepository categoryReadRepository,ICategoryWriteRepository categoryWriteRepository)
        {
            this.categoryReadRepository = categoryReadRepository;
            this.categoryWriteRepository = categoryWriteRepository;
        }

        public async Task CreateCategoryAsync(string name)
        {
          await categoryWriteRepository.AddAsync(new Category()
            {
                Id = new Guid(),
                Name = name
            });
            await categoryWriteRepository.SaveAsync();
        }

        public async Task DeleteCategoryAsync(string id)
        {
            await categoryWriteRepository.RemoveAsync(id);
            await categoryWriteRepository.SaveAsync();
        }

        public async Task<(List<GetAllCategoriesDTO>, int totalCount)> GetAllCategoriesAsync(int page, int size)
        {
            var categories = categoryReadRepository.GetAll().ToList();
            int totalCount = categories.Count;
            List<GetAllCategoriesDTO> categoriesDTOs = categories.Select(c => new GetAllCategoriesDTO()
            {
                Id = c.Id.ToString(),
                Name = c.Name,
                CreatedDate = c.CreatedDate,
                UpdatedDate = c.UpdatedDate
            }).Skip(page*size).Take(size).ToList();
            return new(categoriesDTOs,totalCount);
        }
    }
}
