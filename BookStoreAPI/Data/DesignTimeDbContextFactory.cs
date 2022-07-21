using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace BookStoreAPI.Data
{
    public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<BookStoreContext>
    {
        public BookStoreContext CreateDbContext(string[] args)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile(@Directory.GetCurrentDirectory() + "/appsettings.json").Build();
            var builder = new DbContextOptionsBuilder<BookStoreContext>();
            string connectionString = configuration.GetConnectionString("BookStoreDB");
            builder.UseSqlServer(connectionString);
            return new BookStoreContext(builder.Options);
        }
    }
}
