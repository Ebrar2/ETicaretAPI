using ETicaretAPI.Application.Repositories;
using ETicaretAPI.Application.RequestParmeters;
using ETicaretAPI.Application.ViewModels.Products;
using ETicaretAPI.Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
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

        public ProductController(IProductReadRepository readRepository, IProductWriteRepository writeRepository,IWebHostEnvironment webHostEnvironment)
        {
            this.readRepository = readRepository;
            this.writeRepository = writeRepository;
            this.webHostEnvironment = webHostEnvironment;
        }

        [HttpGet]
        public IActionResult Get([FromQuery] Pagination pagination)
        {
            var totalCount = readRepository.GetAll(false).Count();
            var products = readRepository.GetAll(false).Skip(pagination.Page * pagination.Size).Take(pagination.Size).Select(p => new
            {
                p.Id,
                p.Name,
                p.Price,
                p.Stock,
                p.UpdatedDate,
                p.CreatedDate
            }).ToList();

            return Ok(new
            {
               totalCount,
               products
            });
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(string id)
        {
            return Ok(await readRepository.GetByIdAsync(id,false));
        }
        [HttpPost]
        public async Task<IActionResult> Post(CreateProductVM productVM)
        {
            await writeRepository.AddAsync(new Product()
            {
                Name = productVM.Name,
                Price = productVM.Price,
                Stock = productVM.Stock
            });
            await writeRepository.SaveAsync();
            return Ok();
        }
        [HttpPost("[action]")]
        public  async Task<IActionResult> Upload()
        {
            string uploadPath = Path.Combine(webHostEnvironment.WebRootPath, "reesource\\product-images");
            if (!Directory.Exists(uploadPath))
                Directory.CreateDirectory(uploadPath);
            Random r = new Random();
            foreach(IFormFile file in Request.Form.Files)
            {
                string fullPath = Path.Combine(uploadPath, $"{r.Next()}{Path.GetExtension(file.FileName)}");
                using FileStream fileStream = new FileStream(fullPath, FileMode.Create, FileAccess.Write, FileShare.None, 1024 * 1024, useAsync: false);
                await file.CopyToAsync(fileStream);
                await fileStream.FlushAsync();
            }
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
