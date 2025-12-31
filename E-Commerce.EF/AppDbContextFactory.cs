using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace E_Commerce.EF.Data
{
    public class AppDbContextFactory
        : IDesignTimeDbContextFactory<AppDbContext>
    {
        public AppDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();

            optionsBuilder.UseSqlServer(
                "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=E-Commerce_Web;Integrated Security=True;Connect Timeout=30;Encrypt=False"
            );

            return new AppDbContext(optionsBuilder.Options);
        }
    }
}
