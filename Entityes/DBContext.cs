using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace MVC_Intro.Models
{
    public class DBContext: DbContext
    {
        public DBContext(DbContextOptions<DBContext> options) : base(options)
        {

        }
        public DbSet<BlogModel> Blog { get; set; }

        //public class EFDBContextFactory : IDesignTimeDbContextFactory<DBContext>
        //{
        //    public DBContext CreateDbContext(string[] args)
        //    {
        //        var optionsBuilder = new DbContextOptionsBuilder<DBContext>();
        //        optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=blog;Trusted_Connection=True;MultipleActiveResultSets=true");

        //        return new DBContext(optionsBuilder.Options);
        //    }
        //}
    }
}
