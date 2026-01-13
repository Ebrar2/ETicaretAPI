using ETicaretAPI.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaretAPI.Application.DTOs.Product
{
    public class GetProductById
    {
        public string Name { get; set; }
        public int Stock { get; set; }
        public float Price { get; set; }
        public string[] Categories { get; set; }
    }
     
}
