using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace FoodDesire.DAL.Context;
public class FoodDesireContextFactory : IDesignTimeDbContextFactory<FoodDesireContext> {
    public FoodDesireContext CreateDbContext(string[] args) {
        var config = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.Migrations.json", optional: false, reloadOnChange: true)
            .AddEnvironmentVariables()
            .Build();

        string? connectionString = config.GetConnectionString("DefaultConnection");
        DbContextOptionsBuilder<FoodDesireContext> builder = new DbContextOptionsBuilder<FoodDesireContext>();
        builder.UseSqlServer(connectionString);
        return new FoodDesireContext(builder.Options);
    }
}
