using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaretAPI.Application.Feautures.Queries.Category.GetAll
{
   public  class GetAllCategoriesQueryResponse
    {
        public object Categories { get; set; }
        public int TotalCount { get; set; }
    }
}
