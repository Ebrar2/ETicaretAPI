using ETicaretAPI.Application.Abstractions.Services;
using ETicaretAPI.Application.Abstractions.Storage;
using ETicaretAPI.Application.Attributes;
using ETicaretAPI.Application.Consts;
using ETicaretAPI.Application.Enums;
using ETicaretAPI.Application.Feautures.Commands.Product.CreateProduct;
using ETicaretAPI.Application.Feautures.Commands.Product.DeleteProduct;
using ETicaretAPI.Application.Feautures.Commands.Product.UpdateProduct;
using ETicaretAPI.Application.Feautures.Commands.Product.UpdateProductStock;
using ETicaretAPI.Application.Feautures.Commands.ProductImageFile.ChangeShowcaseProductImage;
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
using Microsoft.AspNetCore.Authorization;
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
    [Authorize(AuthenticationSchemes ="Admin")]
    public class ProductController : ControllerBase
    {



        readonly IStorageService storageService;
        readonly IProductService productService;
        readonly IMediator mediator;
        public ProductController( IStorageService storageService,IMediator mediator,IProductService productService)
        {
        
          
           this.storageService = storageService;
            this.mediator = mediator;
            this.productService = productService;
        }

      

        [HttpGet]
        [AllowAnonymous]
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
        [AuthorizeDefinition(Menu = AuthorizeDefinitionConstants.Products, Definition = "Create Product", ActionTypes = ActionTypes.Writing)]

        public async Task<IActionResult> Post(CreateProductCommandRequest createProductCommandRequest)
        {
            
            return Ok(await mediator.Send(createProductCommandRequest));
        }
        [HttpPut]
        [AuthorizeDefinition(Menu = AuthorizeDefinitionConstants.Products, Definition = "Update Product", ActionTypes = ActionTypes.Updating)]
        public async Task<IActionResult> Put([FromBody]UpdateProductCommanRequest updateProductCommanRequest)
        {
            await mediator.Send(updateProductCommanRequest);
            return Ok();
        }
        [HttpDelete("{Id}")]
        [AuthorizeDefinition(Menu = AuthorizeDefinitionConstants.Products, Definition = "Delete Product", ActionTypes = ActionTypes.Deleting)]
        public async Task<IActionResult> Delete([FromRoute]DeleteProductCommandRequest deleteProductCommandRequest)
        {
            await mediator.Send(deleteProductCommandRequest);
            return Ok();
        }
        [HttpGet("[action]/{Id}")]
        [AuthorizeDefinition(Menu = AuthorizeDefinitionConstants.Products, Definition = "Get Product Images", ActionTypes = ActionTypes.Reading)]
        public async Task<IActionResult> GetProductImages([FromRoute]GetProductImagesQueryRequest getProductImagesQueryRequest)
        {
            return Ok(await mediator.Send(getProductImagesQueryRequest));

        }
        [HttpPost("[action]")]
        [AuthorizeDefinition(Menu = AuthorizeDefinitionConstants.Products, Definition = "Upload Product Images", ActionTypes = ActionTypes.Writing)]
        public async Task<IActionResult> Upload([FromQuery]UploadProductImageCommandRequest uploadProductImageCommandRequest)
        {
            List<(string fileName, string pathOrContainerName)> datas = await storageService.UploadAsync("product-images", Request.Form.Files);
            uploadProductImageCommandRequest.Datas = datas;
            uploadProductImageCommandRequest.StorageName = storageService.StorageName;
            await mediator.Send(uploadProductImageCommandRequest);

            return Ok();
        }
       
        [HttpDelete("[action]/{productId}")]
        [AuthorizeDefinition(Menu = AuthorizeDefinitionConstants.Products, Definition = "Delete Product Images", ActionTypes = ActionTypes.Deleting)]
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
        [HttpGet("[action]")]
        [AuthorizeDefinition(Menu = AuthorizeDefinitionConstants.Products, Definition = "Update Showcase For Product Image", ActionTypes = ActionTypes.Updating)]
        public async Task<IActionResult> ChangeShowcaseForProductImage([FromQuery]ChangeShowcaseForProductImageCommandRequest changeShowcaseForProductImageCommandRequest)
        {
            return Ok(await mediator.Send(changeShowcaseForProductImageCommandRequest));
        }
        [HttpGet("[action]/{productId}")]
        [AuthorizeDefinition(Menu = AuthorizeDefinitionConstants.Products, Definition = "Generate QR Code To Product", ActionTypes = ActionTypes.Reading)]
        public async Task<IActionResult> GenerateQRCodeToProduct([FromRoute]string productId)
        {
            var result = await productService.GenerateQRCodeToProduct(productId);
            return File(result,"image/png");
        }
        [HttpPut("[action]")]
        [AuthorizeDefinition(Menu = AuthorizeDefinitionConstants.Products, Definition = "Change Product Stock", ActionTypes = ActionTypes.Updating)]

        public async Task<IActionResult> ChangeProductStock([FromBody]UpdateProductStockCommandRequest updateProductStockCommandRequest)
        {
            return Ok(await mediator.Send(updateProductStockCommandRequest));

        }
    }
}
