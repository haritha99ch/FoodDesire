using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;

namespace FoodDesire.DAL.Context;
public class ApplicationDbContextFactory : IDesignTimeDbContextFactory<ApplicationDbContext> {
    public ApplicationDbContext CreateDbContext(string[] args) {
        string environmentName = null!;
        //Cannot retrieve env variables without IHost.
        //appsettings.json files cannot be found withing ConfigureAppConfiguration.
        // Had to implement this overkill method for Database Migrations
        IHost? Host = Microsoft.Extensions.Hosting.Host
           .CreateDefaultBuilder()
           .ConfigureAppConfiguration((context, config) =>
                environmentName = context.HostingEnvironment.EnvironmentName).Build();

        ConfigurationBuilder? builder = new ConfigurationBuilder();
        AppSettings.Configure.ConfigureEnvironment(builder, environmentName);
        IConfigurationRoot? config = builder.Build();

        string connectionString = config.GetConnectionString("DefaultConnection")!;

        DbContextOptionsBuilder<ApplicationDbContext> dbBuilder = new();
        dbBuilder.UseSqlServer(connectionString);
        return new ApplicationDbContext(dbBuilder.Options);
    }
}
