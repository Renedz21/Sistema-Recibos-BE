using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Web.Domain.Repositories;
using Web.Infraestructure.Persistence.Configuration;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

namespace Web.Infraestructure.Persistence.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T>, IDisposable where T : class
    {
        private readonly DatabaseContext context;
        public GenericRepository(DatabaseContext context)
        {
            this.context = context;
        }

        public void Dispose()
        {
            if (this.context != null)
            {
                this.context.Dispose();
            }
            GC.SuppressFinalize(this);
        }

        public async Task Add(T entity)
        {
            await this.context.Set<T>().AddAsync(entity);
            await this.context.SaveChangesAsync();

        }

        public Task<List<T>> FindAll()
        {
            return context.Set<T>().ToListAsync();
        }

        public Task<T> FindById(int id)
        {
            return this.context.Set<T>().FindAsync(id).AsTask();
        }
    }
}
