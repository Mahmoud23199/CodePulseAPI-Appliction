using CodePulseBL.Repository;
using CodePulseDB.Models.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodePulseBL.UnitOfWork
{
    public interface IUnitOfWork:IDisposable
    {
        public IGenericRepository<Category> Categorys { get;}
        public IGenericRepository<BlogPost> BlogPosts { get;}

        int Complete();


    }
}
