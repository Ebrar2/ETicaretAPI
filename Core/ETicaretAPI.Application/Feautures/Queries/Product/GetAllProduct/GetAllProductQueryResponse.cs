using ETicaretAPI.Application.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaretAPI.Application.Feautures.Queries.Product.GetAllProduct
{
    public class GetAllProductQueryResponse
    {
        public int totalCount { get; set; }
        public object Products { get; set; }
    }
}
