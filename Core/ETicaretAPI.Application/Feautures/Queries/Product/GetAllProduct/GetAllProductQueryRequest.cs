using ETicaretAPI.Application.RequestParmeters;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaretAPI.Application.Feautures.Queries.Product.GetAllProduct
{
    public class GetAllProductQueryRequest:IRequest<GetAllProductQueryResponse>
    {
        public int Page { get; set; } = 0;
        public int Size { get; set; } = 5;
        public string[]? FilterCategories { get; set; }
        public int? MaxPrice { get; set; }
        public string? Name { get; set; }
    }
}
