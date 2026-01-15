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
        Task<byte[]> GenerateQRCodeToProductAsync(string productId);
        Task ChangeProductStockAsync(string producId, int stock);
        Task<GetProductById> GetProductByIdWithCategoriesAsync(string productId);
        Task UpdateAsync(UpdateProductDTO updateProductDTO);
        Task CreateAsync(CreateProductDTO createProductDTO);
        Task<GetAllProductResponseDTO> GetAllAsync(GetAllProductDTO getAllProductDTO);
    }
}
