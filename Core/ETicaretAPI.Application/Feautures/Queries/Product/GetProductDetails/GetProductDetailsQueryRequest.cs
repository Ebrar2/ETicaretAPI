using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaretAPI.Application.Feautures.Queries.Product.GetProductDetails
{
    public class GetProductDetailsQueryRequest:IRequest<GetProductDetailsQueryResponse>
    {
        public string Id { get; set; }
    }
}
