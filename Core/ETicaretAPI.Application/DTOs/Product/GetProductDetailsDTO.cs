using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaretAPI.Application.DTOs.Product
{
    public class GetProductDetailsDTO
    {
        public string Name { get; set; }
        public int Stock { get; set; }
        public float Price { get; set; }
        public List<Image> Images { get; set; }
    }
    public class Image
    {
        public string Path { get; set; }
        public string FileName { get; set; }
        public Guid Id { get; set; }
        public bool Showcase { get; set; }
    }
 
}
