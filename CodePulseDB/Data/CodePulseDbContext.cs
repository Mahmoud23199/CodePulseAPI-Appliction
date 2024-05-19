using CodePulseDB.Models.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodePulseDB.Data
{
    public class CodePulseDbContext : DbContext
    {
        public DbSet<BlogPost> BlogPosts { get; init; }
        public DbSet<Category> Categories { get; init; }

        public CodePulseDbContext(DbContextOptions<CodePulseDbContext>options):base(options)
        {
        }
    }
}
