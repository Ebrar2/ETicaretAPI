using ETicaretAPI.Application.Attributes;
using ETicaretAPI.Application.Consts;
using ETicaretAPI.Application.Enums;
using ETicaretAPI.Application.Feautures.Commands.Category.CreateCategory;
using ETicaretAPI.Application.Feautures.Commands.Category.DeleteCategory;
using ETicaretAPI.Application.Feautures.Commands.Category.UpdateCategory;
using ETicaretAPI.Application.Feautures.Queries.Category.GetAll;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ETicaretAPI.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes ="Admin")]
    public class CategoriesController : ControllerBase
    {
        readonly IMediator mediator;

        public CategoriesController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpGet("[action]")]
        [AuthorizeDefinition(Menu = AuthorizeDefinitionConstants.Categories, Definition = "Get All Categories", ActionTypes = ActionTypes.Reading)]
        [AllowAnonymous]
        public async Task<IActionResult> GetAllCategories([FromQuery]GetAllCategoriesQueryRequest getAllCategoriesQueryRequest)
        {
            return Ok(await mediator.Send(getAllCategoriesQueryRequest));
        }
        [HttpPost("[action]")]
        [AuthorizeDefinition(Menu = AuthorizeDefinitionConstants.Categories, Definition = "Create Category", ActionTypes = ActionTypes.Writing)]
        public async Task<IActionResult> CreateCategory([FromBody]CreateCategoryCommandRequest createCategoryCommandRequest)
        {
            return Ok(await mediator.Send(createCategoryCommandRequest));
        }
        [HttpDelete("{id}")]
        [AuthorizeDefinition(Menu = AuthorizeDefinitionConstants.Categories, Definition = "Delete Category", ActionTypes = ActionTypes.Deleting)]
        public async Task<IActionResult> Delete([FromRoute]DeleteCategoryCommandRequest deleteCategoryCommandRequest)
        {
            return Ok(await mediator.Send(deleteCategoryCommandRequest));
        }
        [HttpPut]
        [AuthorizeDefinition(Menu = AuthorizeDefinitionConstants.Categories, Definition = "Update Category", ActionTypes = ActionTypes.Updating)]
        public async Task<IActionResult> Put([FromBody]UpdateCategoryCommandRequest updateCategoryCommandRequest)
        {
            return Ok(await mediator.Send(updateCategoryCommandRequest));
        }
    }
}
