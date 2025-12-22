using ETicaretAPI.Application.Feautures.Commands.Order.CreateOrder;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ETicaretAPI.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes ="Admin")]
    public class OrdersController : ControllerBase
    {
        readonly IMediator mediator;

        public OrdersController(IMediator mediator)
        {
            this.mediator = mediator;
        }
        [HttpPost]
        public async Task<IActionResult> CreateOrder([FromBody]CreateOrderCommandRequest createOrderCommandRequest)
        {
            return Ok(await mediator.Send(createOrderCommandRequest));
        }

    }
}
