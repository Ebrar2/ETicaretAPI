using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ETicaretAPI.Domain.Entities
{
     public class ProductImageFile:File
    {
        public bool Showcase { get; set; }
        [JsonIgnore]
        public ICollection<Product> Products { get; set; }
    }
}
