using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CodePulseBL.Repository
{
    public interface IGenericRepository<T>where T : class
    {
        Task<T> GetByIdAsync(Expression<Func<T, bool>> match, string[] includes=null);
        Task<IEnumerable<T>> GetAllAsync(Expression<Func<T,bool>>match,string[] includes = null);
        Task AddedAsync(T entity);
        Task <T>UpdateAsync(T entity);
        Task<bool>DeleteAsync(T entity);



    }
}
