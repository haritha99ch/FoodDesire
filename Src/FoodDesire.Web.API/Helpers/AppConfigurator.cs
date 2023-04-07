using FoodDesire.AppSettings;

namespace FoodDesire.Web.API.Helpers;
internal static class AppConfigurator {
    internal static void Configure(WebApplicationBuilder builder) {
        string environmentName = builder.Environment.EnvironmentName;
        AppSettings.Configure.ConfigureEnvironment(builder.Configuration, environmentName);
        ConfigureServices(builder);
    }

    internal static void ConfigureServices(WebApplicationBuilder builder) {
        // Core Services
        string connectionString = builder.Configuration.GetConnectionString("DefaultConnection")!;
        DAL.Configure.ConfigureServices(builder.Services, connectionString);
        Core.Configure.ConfigureServices(builder.Services);
    }
}
