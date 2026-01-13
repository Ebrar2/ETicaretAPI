using ETicaretAPI.Application.DTOs.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaretAPI.Application.Abstractions.Services
{
    public interface IProductService
    {
        Task<byte[]> GenerateQRCodeToProduct(string productId);
        Task ChangeProductStock(string producId, int stock);
        Task<GetProductById> GetProductByIdWithCategories(string productId);
        Task Update(UpdateProductDTO updateProductDTO);
        Task Create(CreateProductDTO createProductDTO);
    }
}
