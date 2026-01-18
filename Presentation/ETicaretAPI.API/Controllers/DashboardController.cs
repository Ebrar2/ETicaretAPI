using ETicaretAPI.Application.Abstractions.Services;
using ETicaretAPI.Application.Abstractions.Storage;
using ETicaretAPI.Application.Attributes;
using ETicaretAPI.Application.Consts;
using ETicaretAPI.Application.Enums;
using ETicaretAPI.Application.Feautures.Commands.Product.CreateProduct;
using ETicaretAPI.Application.Feautures.Queries.Order.GetDashboardData;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ETicaretAPI.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = "Admin")]

    public class DashboardController : ControllerBase
    {

        readonly IMediator mediator;
        public DashboardController(IMediator mediator)
        {

            this.mediator = mediator;
        }

        [HttpGet]
        [AuthorizeDefinition(Menu = AuthorizeDefinitionConstants.Dashboard, Definition = "Get Datas of Dashboard", ActionTypes = ActionTypes.Reading)]

        public async Task<IActionResult> Get([FromQuery] GetDashboardDataQueryRequest getDashboardDataQueryRequest)
        {
            return Ok(await mediator.Send(getDashboardDataQueryRequest));

        }
    }
}