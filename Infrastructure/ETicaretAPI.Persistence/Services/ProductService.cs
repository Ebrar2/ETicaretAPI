using ETicaretAPI.Application.Abstractions.Services;
using ETicaretAPI.Application.DTOs.Product;
using ETicaretAPI.Application.Repositories;
using ETicaretAPI.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace ETicaretAPI.Persistence.Services
{
    public class ProductService : IProductService
    {
        readonly IProductReadRepository productReadRepository;
        readonly IQRCodeService qRCodeService;
        readonly IProductWriteRepository productWriteRepository;
        readonly ICategoryReadRepository categoryReadRepository;

        public ProductService(IProductReadRepository productReadRepository, IQRCodeService qRCodeService,IProductWriteRepository productWriteRepository,ICategoryReadRepository categoryReadRepository)
        {
            this.productReadRepository = productReadRepository;
            this.qRCodeService = qRCodeService;
            this.productWriteRepository = productWriteRepository;
            this.categoryReadRepository = categoryReadRepository;
        }

        public async Task ChangeProductStock(string producId, int stock)
        {
            var product = await productReadRepository.GetByIdAsync(producId);
            product.Stock = stock;
            await productWriteRepository.SaveAsync();
        }

        public async Task Create(CreateProductDTO createProductDTO)
        {
            List<Category> categories = new List<Category>();
            foreach(var categoryId in createProductDTO.Categories)
            {
                var category = await categoryReadRepository.GetByIdAsync(categoryId);
                categories.Add(category);
            }
            await productWriteRepository.AddAsync(new Domain.Entities.Product()
            {
                Name = createProductDTO.Name,
                Price = createProductDTO.Price,
                Stock = createProductDTO.Stock,
                Categories=categories
            });
            await productWriteRepository.SaveAsync();
        }

        public async Task<byte[]> GenerateQRCodeToProduct(string productId)
        {
            var product = await productReadRepository.GetByIdAsync(productId);
            var productObject = new
            {
                product.Id,
                product.Name,
                product.Price,
                product.Stock,
                product.CreatedDate
            };
            string text = JsonSerializer.Serialize(productObject);
           return qRCodeService.GenerateQRCode(text);
        }

        public async Task<GetProductById> GetProductByIdWithCategories(string productId)
        {
            var product = await productReadRepository.Table.Include(p => p.Categories).FirstOrDefaultAsync(p=>p.Id==Guid.Parse(productId));
            string[] categoryNames = product.Categories.Select(c => c.Name).ToArray();
            return new()
            {
                Name = product.Name,
                Price = product.Price,
                Stock = product.Stock,
                Categories=categoryNames
            };
        }

        public async Task Update(UpdateProductDTO updateProductDTO)
        {
            var product = await productReadRepository.Table.Include(p=>p.Categories).FirstOrDefaultAsync(p => p.Id == Guid.Parse(updateProductDTO.Id));
            product.Stock = updateProductDTO.Stock;
            product.Price = updateProductDTO.Price;
            product.Name = updateProductDTO.Name;
            foreach (var category in product.Categories)
                product.Categories.Remove(category);
            foreach (var categoryId in updateProductDTO.Categories)
            {
                var category = await categoryReadRepository.GetByIdAsync(categoryId);
                product.Categories.Add(category);
            }
            await productWriteRepository.SaveAsync();
        }
    }
}
