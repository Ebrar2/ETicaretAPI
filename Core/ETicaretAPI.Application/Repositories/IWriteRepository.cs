using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaretAPI.Application.Repositories
{
    public interface IWriteRepository<T>:IRepository<T> where T:class
    {
        Task<bool> AddAsync(T t);
        Task<bool>  AddRangeAsync(List<T> values);
        bool Update(T t);

        Task<bool> RemoveAsync(string id);
        bool RemoveRange(List<T> values);
        bool Remove(T t);
        Task<int> SaveAsync();

    }
}
