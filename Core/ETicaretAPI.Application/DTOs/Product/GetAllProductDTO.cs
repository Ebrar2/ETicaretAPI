using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaretAPI.Application.DTOs.Product
{
    public class GetAllProductDTO
    {
        public int Page { get; set; } = 0;
        public int Size { get; set; } = 5;
        public string?[] FilterCategories { get; set; }
        public int? MaxPrice { get; set; }
        public string? Name { get; set; }
        public bool? IsAscending { get; set; }

    }
}
