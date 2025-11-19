using ETicaretAPI.Application.Repositories;
using ETicaretAPI.Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace ETicaretAPI.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {

        IProductReadRepository readRepository;
        IProductWriteRepository writeRepository;
        ICustomerReadRepository customerReadRepository;
        ICustomerWriteRepository customerWriteRepository;
        IOrderWriteRepository orderWriteRepository;
        IOrderReadRepository orderReadRepository;

        public ProductController(IProductReadRepository readRepository, IProductWriteRepository writeRepository, ICustomerReadRepository customerReadRepository, ICustomerWriteRepository customerWriteRepository, IOrderWriteRepository orderWriteRepository, IOrderReadRepository orderReadRepository)
        {
            this.readRepository = readRepository;
            this.writeRepository = writeRepository;
            this.customerReadRepository = customerReadRepository;
            this.customerWriteRepository = customerWriteRepository;
            this.orderWriteRepository = orderWriteRepository;
            this.orderReadRepository = orderReadRepository;
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
        //}
        [HttpGet]

        public async Task<IActionResult> Get()
        {
            //return Ok(await readRepository.GetByIdAsync(id));

            // Guid id = Guid.NewGuid();
            //await customerWriteRepository.AddAsync(new Customer()
            // {
            //     Id=id,
            //     Name="Customer1"
            // });
            // await orderWriteRepository.AddAsync(new Order()
            // {
            //     Address = "İzmit",
            //     CustomerId = id,
            //     Description="Descriptiın",
            // });
            // await orderWriteRepository.AddAsync(new Order()
            // {
            //     Address = "İstanbul",
            //     CustomerId = id,
            //     Description = "Descriptiın2",
            // });

            var order = await orderReadRepository.GetByIdAsync("7bd6653f-bf25-440d-ab2e-28e9cfafd2e7");
            order.Address = "Bursa";
            await  orderWriteRepository.SaveAsync();
            return Ok();
        }
    }
}
