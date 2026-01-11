using ETicaretAPI.Application.Abstractions.Services;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaretAPI.Application.Feautures.Queries.Category.GetAll
{
    public class GetAllCategoriesQueryHandler : IRequestHandler<GetAllCategoriesQueryRequest, GetAllCategoriesQueryResponse>
    {
        readonly ICategoryService categoryService;

        public GetAllCategoriesQueryHandler(ICategoryService categoryService)
        {
            this.categoryService = categoryService;
        }

        public async Task<GetAllCategoriesQueryResponse> Handle(GetAllCategoriesQueryRequest request, CancellationToken cancellationToken)
        {
            var (categories, totalCount) = await categoryService.GetAllCategoriesAsync(request.Page, request.Size);
            return new() { Categories = categories, TotalCount = totalCount };
        }
    }
}
