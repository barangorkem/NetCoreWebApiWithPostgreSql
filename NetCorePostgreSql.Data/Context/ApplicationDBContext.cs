using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using NetCorePostgreSql.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace NetCorePostgreSql.Data.Context
{
    public class ApplicationDBContext:DbContext
    {
      
        public ApplicationDBContext(DbContextOptions options) : base(options)
        {

        }
        public class ProductsDbContextFactory : IDesignTimeDbContextFactory<ApplicationDBContext>
        {
            public ApplicationDBContext CreateDbContext(string[] args)
            {
                var optionsBuilder = new DbContextOptionsBuilder<ApplicationDBContext>();
                var connectionString = "Host=localhost;Port=5432;Username=postgres;Password=123456789;Database=NetCorePostgreSqlDatabase;";
                optionsBuilder.UseNpgsql(connectionString);
                return new ApplicationDBContext(optionsBuilder.Options);
            }
        }
        public DbSet<Users> Users { get; set; }
    }
}
