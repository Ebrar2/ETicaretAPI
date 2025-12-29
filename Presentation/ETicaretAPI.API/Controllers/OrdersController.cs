using ETicaretAPI.Application.Feautures.Commands.Order.CompleteOrder;
using ETicaretAPI.Application.Feautures.Commands.Order.CreateOrder;
using ETicaretAPI.Application.Feautures.Queries.Order;
using ETicaretAPI.Application.Feautures.Queries.Order.GetOrderDetailsById;
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
        [HttpGet]
        public async Task<IActionResult> GetOrder([FromQuery]GetOrdersQueryRequest getOrdersQueryRequest)
        {
            return Ok(await mediator.Send(getOrdersQueryRequest));
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetOrderDetailsById([FromRoute] GetOrderDetailsByIdQueryRequest orderDetailsByIdQueryRequest)
        {
            return Ok(await mediator.Send(orderDetailsByIdQueryRequest));
        }
        [HttpGet("[action]/{id}")]
        public async Task<IActionResult> CompleteOrder([FromRoute]CompleteOrderCommandRequest completeOrderCommandRequest )
        {
            return Ok(await mediator.Send(completeOrderCommandRequest));
        }

    }
}
