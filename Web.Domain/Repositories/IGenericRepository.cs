using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Web.Domain.Repositories
{
    public interface IGenericRepository<T> where T : class
    {
        Task<List<T>> FindAll();
        Task<T> FindById(int id);
        Task Add(T entity);
    }
}
