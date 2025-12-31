using ETicaretAPI.Application.Attributes;
using ETicaretAPI.Application.Consts;
using ETicaretAPI.Application.Enums;
using ETicaretAPI.Application.Feautures.Commands.Basket.AddItemToBasketItem;
using ETicaretAPI.Application.Feautures.Commands.Basket.RemoveBasketItem;
using ETicaretAPI.Application.Feautures.Commands.Basket.UpdateQuantity;
using ETicaretAPI.Application.Feautures.Queries.Basket.GetBasketItems;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ETicaretAPI.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes ="Admin")]
    public class BasketsController : ControllerBase
    {
        readonly IMediator mediator;

        public BasketsController(IMediator mediator)
        {
            this.mediator = mediator;
        }
        [HttpGet]
        [AuthorizeDefinition(Menu =AuthorizeDefinitionConstants.Baskets,Definition ="Get Basket Items",ActionTypes =ActionTypes.Reading)]
        public async Task<IActionResult> GetBasketItems([FromQuery]GetBasketItemsQueryRequest getBasketItemsQueryRequest)
        {
            return Ok(await mediator.Send(getBasketItemsQueryRequest));
        }
        [HttpPost]
        [AuthorizeDefinition(Menu=AuthorizeDefinitionConstants.Baskets,Definition ="Add Item to Basket",ActionTypes =ActionTypes.Writing)]
        public async Task<IActionResult> AddItemToBasket(AddItemToBasketItemCommandRequest addItemToBasket)
        {
            return Ok(await mediator.Send(addItemToBasket));

        }
        [HttpPut]
        [AuthorizeDefinition(Menu =AuthorizeDefinitionConstants.Baskets,Definition ="Update Quantity",ActionTypes =ActionTypes.Updating)]
        public async Task<IActionResult> UpdateQuantity(UpdateQuantityCommandRequest updateQuantityCommandRequest)
        {
            return Ok(await mediator.Send(updateQuantityCommandRequest));

        }
        [HttpDelete("{basketItemId}")]
        [AuthorizeDefinition(Menu =AuthorizeDefinitionConstants.Baskets,Definition ="Delete Basket Item",ActionTypes =ActionTypes.Deleting)]
        public async Task<IActionResult> DeleteBasketItem([FromRoute]RemoveBasketItemCommandRequest removeBasketItemCommand)
        {
            return Ok(await mediator.Send(removeBasketItemCommand));

        }
    }
}
