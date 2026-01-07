using ETicaretAPI.Application.Abstractions.Services;
using ETicaretAPI.Application.Repositories;
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

        public ProductService(IProductReadRepository productReadRepository, IQRCodeService qRCodeService,IProductWriteRepository productWriteRepository)
        {
            this.productReadRepository = productReadRepository;
            this.qRCodeService = qRCodeService;
            this.productWriteRepository = productWriteRepository;
        }

        public async Task ChangeProductStock(string producId, int stock)
        {
            var product = await productReadRepository.GetByIdAsync(producId);
            product.Stock = stock;
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
    }
}
