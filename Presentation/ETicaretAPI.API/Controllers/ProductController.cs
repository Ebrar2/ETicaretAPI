using ETicaretAPI.Application.Abstractions.Storage;
using ETicaretAPI.Application.Feautures.Commands.Product.CreateProduct;
using ETicaretAPI.Application.Feautures.Commands.Product.DeleteProduct;
using ETicaretAPI.Application.Feautures.Commands.Product.UpdateProduct;
using ETicaretAPI.Application.Feautures.Commands.ProductImageFile.DeleteProductImage;
using ETicaretAPI.Application.Feautures.Commands.ProductImageFile.UploadProductImage;
using ETicaretAPI.Application.Feautures.Queries.Product.GetAllProduct;
using ETicaretAPI.Application.Feautures.Queries.Product.GetByIdProduct;
using ETicaretAPI.Application.Feautures.Queries.ProductImageFile.GetProductImages;
using ETicaretAPI.Application.Repositories;
using ETicaretAPI.Application.RequestParmeters;
using ETicaretAPI.Application.ViewModels.Products;
using ETicaretAPI.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Azure;
using System.Threading.Tasks;

namespace ETicaretAPI.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {



        readonly IStorageService storageService;

        readonly IMediator mediator;
        public ProductController( IStorageService storageService,IMediator mediator)
        {
        
          
           this.storageService = storageService;
            this.mediator = mediator;
        }

      

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] GetAllProductQueryRequest getAllProductQueryRequest)
        {
            return Ok( await mediator.Send(getAllProductQueryRequest));
        }
        [HttpGet("{Id}")]
        public async Task<IActionResult> Get([FromRoute] GetByIdProductQueryRequest getByIdProductQueryRequest)
        {
            return Ok(await mediator.Send(getByIdProductQueryRequest));
        }
        [HttpPost]
        public async Task<IActionResult> Post(CreateProductCommandRequest createProductCommandRequest)
        {
            
            return Ok(await mediator.Send(createProductCommandRequest));
        }
        [HttpPut]
        public async Task<IActionResult> Put([FromBody]UpdateProductCommanRequest updateProductCommanRequest)
        {
            await mediator.Send(updateProductCommanRequest);
            return Ok();
        }
        [HttpDelete("{Id}")]
        public async Task<IActionResult> Delete([FromRoute]DeleteProductCommandRequest deleteProductCommandRequest)
        {
            await mediator.Send(deleteProductCommandRequest);
            return Ok();
        }
        [HttpGet("[action]/{Id}")]
        public async Task<IActionResult> GetProductImages([FromRoute]GetProductImagesQueryRequest getProductImagesQueryRequest)
        {
            return Ok(await mediator.Send(getProductImagesQueryRequest));

        }
        [HttpPost("[action]")]
        public async Task<IActionResult> Upload([FromQuery]UploadProductImageCommandRequest uploadProductImageCommandRequest)
        {
            List<(string fileName, string pathOrContainerName)> datas = await storageService.UploadAsync("product-images", Request.Form.Files);
            uploadProductImageCommandRequest.Datas = datas;
            uploadProductImageCommandRequest.StorageName = storageService.StorageName;
            await mediator.Send(uploadProductImageCommandRequest);

            return Ok();
        }
       
        [HttpDelete("[action]/{productId}")]
        public async Task<IActionResult> DeleteProductImage(string productId,string imageId)
        {
            DeleteProductImageCommandRequest deleteProductImageCommandRequest = new()
            {
                ProductId = productId,
                ImageId = imageId
            };
            await mediator.Send(deleteProductImageCommandRequest);
            return Ok();
            
        }
       
    }
}
