using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppShopping_Data.EF
{
    public class AppShoppingDbContextFactory : IDesignTimeDbContextFactory<AppShoppingDbContext>
    {
        public AppShoppingDbContext CreateDbContext(string[] args)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            var connectionString = configuration.GetConnectionString("appShoppingDb");

            var optionsBuilder = new DbContextOptionsBuilder<AppShoppingDbContext>();
            optionsBuilder.UseSqlServer(connectionString);

            return new AppShoppingDbContext(optionsBuilder.Options);
        }
    }
}
