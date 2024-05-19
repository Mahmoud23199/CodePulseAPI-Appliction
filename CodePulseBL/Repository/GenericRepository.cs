using CodePulseDB.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CodePulseBL.Repository
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly CodePulseDbContext _context;

        public GenericRepository(CodePulseDbContext context) 
        {
            this._context=context;

        }

        public async Task AddedAsync(T entity)
        {
            await _context.Set<T>().AddAsync(entity);
             _context.SaveChanges();
        }

        public async Task<bool> DeleteAsync(T entity)
        {
            try
            {
                _context.Set<T>().Remove(entity);
                await _context.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>> match, string[] includes = null)
        {
            IQueryable<T> query = _context.Set<T>();
            
            if (includes != null)
            {
                foreach (var include in includes) {
                    query =  query.Include(include);
                } 
            }
            return await query.Where(match).ToListAsync();
        }

        public async Task<T> GetByIdAsync(Expression<Func<T,bool>> match, string[] includes = null)
        {
            IQueryable<T> query = _context.Set<T>();
            if(includes != null) 
            {
                foreach (var include in includes) {
                    query = query.Include(include);
                }
            }
            return query.FirstOrDefault(match);

        }

        public async Task<T> UpdateAsync(T entity)
        {
           _context.Set<T>().Entry(entity).State = EntityState.Modified;
           await _context.SaveChangesAsync();
            return entity;
        }
    }
}
