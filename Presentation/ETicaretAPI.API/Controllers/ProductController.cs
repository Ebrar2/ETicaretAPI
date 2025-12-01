using ETicaretAPI.Application.Abstractions.Storage;
using ETicaretAPI.Application.Feautures.Commands.CreateProduct;
using ETicaretAPI.Application.Feautures.Queries.GetAllProduct;
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

        IProductReadRepository readRepository;
        IProductWriteRepository writeRepository;
        IWebHostEnvironment webHostEnvironment;
        IFileWriteRepository fileWriteRepository;
        IFileReadRepository fileReadRepository;
        IProductImageFileReadRepository productImageFileReadRepository;
        IProductImageFileWriteRepository productImageFileWriteRepository;
        IInvoiceFileReadRepository invoiceFileReadRepository;
        IInvoiceFileWriteRepository invoiceFileWriteRepository;
        IStorageService storageService;
        IConfiguration configuration;

        readonly IMediator mediator;
        public ProductController(IProductReadRepository readRepository, IProductWriteRepository writeRepository, IWebHostEnvironment webHostEnvironment, IFileWriteRepository fileWriteRepository, IFileReadRepository fileReadRepository, IProductImageFileReadRepository productImageFileReadRepository, IProductImageFileWriteRepository productImageFileWriteRepository, IInvoiceFileReadRepository invoiceFileReadRepository, IInvoiceFileWriteRepository invoiceFileWriteRepository, IStorageService storageService, IConfiguration configuration,IMediator mediator)
        {
            this.readRepository = readRepository;
            this.writeRepository = writeRepository;
            this.webHostEnvironment = webHostEnvironment;
            this.fileWriteRepository = fileWriteRepository;
            this.fileReadRepository = fileReadRepository;
            this.productImageFileReadRepository = productImageFileReadRepository;
            this.productImageFileWriteRepository = productImageFileWriteRepository;
            this.invoiceFileReadRepository = invoiceFileReadRepository;
            this.invoiceFileWriteRepository = invoiceFileWriteRepository;
            this.storageService = storageService;
            this.configuration = configuration;
            this.mediator = mediator;
        }

      

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] GetAllProductQueryRequest getAllProductQueryRequest)
        {
            return Ok( await mediator.Send(getAllProductQueryRequest));
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(string id)
        {
            return Ok(await readRepository.GetByIdAsync(id,false));
        }
        [HttpPost]
        public async Task<IActionResult> Post(CreateProductCommandRequest createProductCommandRequest)
        {
            
            return Ok(await mediator.Send(createProductCommandRequest));
        }
        [HttpPost("[action]")]
        public  async Task<IActionResult> Upload(string id)
        { 
            List<(string fileName,string pathOrContainerName)> datas =await storageService.UploadAsync("product-images", Request.Form.Files);
            Product product = await readRepository.GetByIdAsync(id);
            await productImageFileWriteRepository.AddRangeAsync(datas.Select(f => new ProductImageFile()
            {
                FileName=f.fileName,
                Path=f.pathOrContainerName,
                Storage=storageService.StorageName,
                Products = new List<Product>() { product}
            }).ToList());

            await productImageFileWriteRepository.SaveAsync();
        
            return Ok();
        }
        [HttpGet("[action]/{id}")]
        public async Task<IActionResult> GetProductImages(string id)
        {
            Product product=await readRepository.Table.Include(p => p.ProductImageFiles).FirstOrDefaultAsync(p => p.Id ==Guid.Parse(id));
            return Ok(product.ProductImageFiles.Select(p => new
            {
                Path = $"{configuration["BaseStorageUrl"]}/{p.Path}",
                p.FileName,p.Id
            }));
        }
        [HttpDelete("[action]/{productId}")]
        public async Task<IActionResult> DeleteProductImage(string productId,string imageId)
        {
            Product product = await readRepository.Table.Include(p => p.ProductImageFiles).FirstOrDefaultAsync(p => p.Id == Guid.Parse(productId));
            var deleteImage = product.ProductImageFiles.FirstOrDefault(p => p.Id == Guid.Parse(imageId));
            product.ProductImageFiles.Remove(deleteImage);
            await writeRepository.SaveAsync();
            return Ok();
        }
        [HttpPut]
        public async Task<IActionResult> Put(UpdateProductVM updateProduct)
        {
            var product = await readRepository.GetByIdAsync(updateProduct.Id);
            product.Stock = updateProduct.Stock;
            product.Price = updateProduct.Price;
            product.Name = updateProduct.Name;
            await writeRepository.SaveAsync();
            return Ok();
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
          await  writeRepository.RemoveAsync(id);
            await writeRepository.SaveAsync();
            return Ok();
        }
        //[HttpGet]

        //public async Task<IActionResult> GetProduct()
        //{
        //    //await writeRepository.AddRangeAsync(new List<Domain.Entities.Product>()
        //    // {
        //    //     new Domain.Entities.Product(){Id=Guid.NewGuid(), Name="Product1",CreatedDate=DateTime.UtcNow,Stock=100,Price=100},
        //    //     new Domain.Entities.Product(){Id=Guid.NewGuid(),  Name="Product2",CreatedDate=DateTime.UtcNow,Stock=200,Price=200},
        //    //     new Domain.Entities.Product(){Id = Guid.NewGuid(), Name="Product3",CreatedDate=DateTime.UtcNow,Stock=300,Price=300},
        //    //     new Domain.Entities.Product(){Id = Guid.NewGuid(), Name="Product4",CreatedDate=DateTime.UtcNow,Stock=400,Price=400},
        //    //     new Domain.Entities.Product(){Id = Guid.NewGuid(), Name="Product5",CreatedDate=DateTime.UtcNow,Stock=500,Price=500},
        //    // });
        //    // await writeRepository.SaveAsync();
        //    // return Ok(readRepository.GetAll().ToList());

        //    //Product p =await readRepository.GetByIdAsync("c7bf57e1-1035-4821-9b62-a81dc4f0462f");
        //    //p.Name = "Product8";
        //    //await writeRepository.SaveAsync();
        //    //return Ok();
        ////}
        //[HttpGet]

        //public async Task<IActionResult> Get()
        //{
        //    //return Ok(await readRepository.GetByIdAsync(id));

        //    // Guid id = Guid.NewGuid();
        //    //await customerWriteRepository.AddAsync(new Customer()
        //    // {
        //    //     Id=id,
        //    //     Name="Customer1"
        //    // });
        //    // await orderWriteRepository.AddAsync(new Order()
        //    // {
        //    //     Address = "İzmit",
        //    //     CustomerId = id,
        //    //     Description="Descriptiın",
        //    // });
        //    // await orderWriteRepository.AddAsync(new Order()
        //    // {
        //    //     Address = "İstanbul",
        //    //     CustomerId = id,
        //    //     Description = "Descriptiın2",
        //    // });

        //    //var order = await orderReadRepository.GetByIdAsync("7bd6653f-bf25-440d-ab2e-28e9cfafd2e7");
        //    //order.Address = "Bursa";
        //    ////await  orderWriteRepository.SaveAsync();
        //    //await writeRepository.AddRangeAsync(new List<Product>()
        //    //     {
        //    //         new Product(){Id=Guid.NewGuid(), Name="Product1",CreatedDate=DateTime.UtcNow,Stock=100,Price=100},
        //    //         new Product(){Id=Guid.NewGuid(),  Name="Product2",CreatedDate=DateTime.UtcNow,Stock=200,Price=200},
        //    //         new Product(){Id = Guid.NewGuid(), Name="Product3",CreatedDate=DateTime.UtcNow,Stock=300,Price=300},
        //    //         new Product(){Id = Guid.NewGuid(), Name="Product4",CreatedDate=DateTime.UtcNow,Stock=400,Price=400},
        //    //         new Product(){Id = Guid.NewGuid(), Name="Product5",CreatedDate=DateTime.UtcNow,Stock=500,Price=500},
        //    //     });
        //    //await writeRepository.SaveAsync();
        //    return Ok(readRepository.GetAll().ToList());


        //}
    }
}
