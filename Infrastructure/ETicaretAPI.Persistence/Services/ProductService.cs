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

        public ProductService(IProductReadRepository productReadRepository, IQRCodeService qRCodeService)
        {
            this.productReadRepository = productReadRepository;
            this.qRCodeService = qRCodeService;
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
