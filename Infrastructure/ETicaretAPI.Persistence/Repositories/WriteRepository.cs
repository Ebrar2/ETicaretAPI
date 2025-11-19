using ETicaretAPI.Application.Repositories;
using ETicaretAPI.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaretAPI.Persistence.Repositories
{
    public class WriteRepository<T> : IWriteRepository<T> where T : class
    {
        private readonly ETicaretAPIDbContext context;

        public WriteRepository(ETicaretAPIDbContext context)
        {
            this.context = context;
        }

        public DbSet<T> Table => context.Set<T>();

        public async Task<bool> AddAsync(T t)
        {

           EntityEntry<T> entityEntry= await Table.AddAsync(t);

            return entityEntry.State == EntityState.Added;
        }

        public async Task<bool> AddRangeAsync(List<T> values)
        {
            await Table.AddRangeAsync(values);
            return true;
        }

        public async Task<bool> RemoveAsync(string id)
        {
            T t = await Table.FindAsync(Guid.Parse(id));
            return Remove(t);
        }

        public bool Remove(T t)
        {
            EntityEntry<T> entityEntry = Table.Remove(t);
            return entityEntry.State== EntityState.Deleted;
        }

        public bool RemoveRange(List<T> values)
        {
            Table.RemoveRange(values);
            return true;
        }

        public async Task<int> SaveAsync()
        =>await context.SaveChangesAsync();
        public bool Update(T t)
        {
            EntityEntry<T> entityEntry = Table.Update(t);
            return entityEntry.State == EntityState.Modified;
        }
    }
}
