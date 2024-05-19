using CodePulseBL.Repository;
using CodePulseDB.Data;
using CodePulseDB.Models.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodePulseBL.UnitOfWork
{
    public class UnitOfWorkRepo : IUnitOfWork
    {
        private readonly CodePulseDbContext _context;

        public IGenericRepository<Category> Categorys { get; private set; }

        public IGenericRepository<BlogPost> BlogPosts { get; private set; }

        public UnitOfWorkRepo(CodePulseDbContext context)
        {
            this._context=context;

            this.Categorys = new GenericRepository<Category>(context);
            this.BlogPosts=new GenericRepository<BlogPost>(context);
        }

        public int Complete()
        {
           return _context.SaveChanges();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
