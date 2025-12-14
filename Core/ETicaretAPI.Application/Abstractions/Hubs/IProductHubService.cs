using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaretAPI.Application.Abstractions.Hubs
{
   public interface IProductHubService
    {
        public Task ProductAddedAsync(string message);
    }
}
