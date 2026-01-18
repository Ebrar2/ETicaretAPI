using ETicaretAPI.Application.Abstractions.Services;
using ETicaretAPI.Application.DTOs.Product;
using ETicaretAPI.Application.Feautures.Queries.Product.GetAllProduct;
using ETicaretAPI.Application.Feautures.Queries.ProductImageFile.GetProductImages;
using ETicaretAPI.Application.Helpers;
using ETicaretAPI.Application.Repositories;
using ETicaretAPI.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
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
        readonly IConfiguration configuration;

        public ProductService(IProductReadRepository productReadRepository, IQRCodeService qRCodeService,IProductWriteRepository productWriteRepository,ICategoryReadRepository categoryReadRepository,IConfiguration configuration)
        {
            this.productReadRepository = productReadRepository;
            this.qRCodeService = qRCodeService;
            this.productWriteRepository = productWriteRepository;
            this.categoryReadRepository = categoryReadRepository;
            this.configuration = configuration;
        }

        public async Task ChangeProductStockAsync(string producId, int stock)
        {
            var product = await productReadRepository.GetByIdAsync(producId);
            product.Stock = stock;
            await productWriteRepository.SaveAsync();
        }

        public async Task CreateAsync(CreateProductDTO createProductDTO)
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

        public async Task<byte[]> GenerateQRCodeToProductAsync(string productId)
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

        public async Task<GetAllProductResponseDTO> GetAllAsync(GetAllProductDTO getAllProductDTO)
        {
            var allProducts = await productReadRepository.Table.Include(p => p.ProductImageFiles).Include(p => p.Categories).ToListAsync();
            if(getAllProductDTO.FilterCategories!=null)
            {
                allProducts = allProducts.Where(p => p.Categories.Any(c => getAllProductDTO.FilterCategories.Any(s=>s==c.Name))).ToList();
            }
            if (getAllProductDTO.MaxPrice != null)
            {
                allProducts = allProducts.Where(p => p.Price < getAllProductDTO.MaxPrice).ToList();
            }
            if (getAllProductDTO.Name != null && getAllProductDTO.Name.Length!=0)
            {
                allProducts = allProducts.Select(p => new
                {
                    Products = p,
                    Score = p.Name.Similarity(getAllProductDTO.Name)
                })
                .Where(x => x.Score > 0.7)  
                .OrderByDescending(x => x.Score)
                .Select(x => x.Products)
                .ToList();
            }
            if(getAllProductDTO.IsAscending!=null)
            {
                if (getAllProductDTO.IsAscending == true)
                    allProducts = allProducts.OrderBy(p => p.Price).ToList();
                else
                    allProducts = allProducts.OrderByDescending(p => p.Price).ToList();
            }
             var totalCount = allProducts.Count();
            var products = allProducts.Skip(getAllProductDTO.Page * getAllProductDTO.Size).Take(getAllProductDTO.Size).Select(p => new
            {
                p.Id,
                p.Name,
                p.Price,
                p.Stock,
                p.UpdatedDate,
                p.CreatedDate,
                p.ProductImageFiles
            }).ToList();

            return new GetAllProductResponseDTO()
            {
                TotalCount = totalCount,
                Products = products
            };
        }

        public async Task<GetProductById> GetProductByIdWithCategoriesAsync(string productId)
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

        public async Task<GetProductDetailsDTO> GetProductDetailsAsync(string id)
        {

           var product = await productReadRepository.Table.Include(p => p.ProductImageFiles).FirstOrDefaultAsync(p => p.Id == Guid.Parse(id));


           var productImages=  product.ProductImageFiles.Select(p => new Image
            {
                Path = $"{configuration["BaseStorageUrl"]}/{p.Path}",
                FileName = p.FileName,
                Showcase = p.Showcase,
                Id = p.Id
            }).ToList();
            return new GetProductDetailsDTO()
            {
                Name = product.Name,
                Price = product.Price,
                Stock = product.Stock,
                Images=productImages
            };
        }

        public async Task UpdateAsync(UpdateProductDTO updateProductDTO)
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
