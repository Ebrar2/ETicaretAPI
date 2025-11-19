using ETicaretAPI.Application.Repositories;
using ETicaretAPI.Domain.Entities.Common;
using ETicaretAPI.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ETicaretAPI.Persistence.Repositories
{
    public class ReadRepository<T> : IReadRepository<T> where T : BaseEntity
    {
        private readonly ETicaretAPIDbContext context;

        public ReadRepository(ETicaretAPIDbContext context)
        {
            this.context = context;
        }

        public DbSet<T> Table => context.Set<T>();

        public IQueryable<T> GetAll(bool tracking = true)
        {
            var query = Table.AsQueryable();
            if (!tracking)
                query=query.AsNoTracking();
            return Table;
        }
        public async Task<T> GetByIdAsync(string id, bool tracking = true)
        {
            var query = Table.AsQueryable();
            if (!tracking)
                 query=query.AsNoTracking();
            return await query.FirstOrDefaultAsync(x =>x.Id==Guid.Parse(id));
        }
        public async Task<T> GetSingleAsync(Expression<Func<T, bool>> expression,bool tracking=true)
        {
            var query = Table.AsQueryable();
            if (!tracking)
                query=query.AsNoTracking();
             return await query.FirstOrDefaultAsync(expression);
        }
       
        public IQueryable<T> GetWhere(System.Linq.Expressions.Expression<Func<T, bool>> expression,bool tracking=true)
        {
            var query= Table.Where(expression);
            if (!tracking)
                query.AsNoTracking();
            return query;
        }

        
    }
}
