namespace FoodDesire.Web.API.Helpers;
internal static class AppConfigurator {
    internal static void Configure(WebApplicationBuilder builder) {
        AppSettings.Configure.ConfigureEnvironment(builder.Configuration);
        ConfigureServices(builder);
    }

    internal static void ConfigureServices(WebApplicationBuilder builder) {
        // Core Services
        string connectionString = builder.Configuration.GetConnectionString("DefaultConnection")!;
        DAL.Configure.ConfigureServices(builder.Services, connectionString);
        Core.Configure.ConfigureServices(builder.Services);
        ML.Model.Configure.ConfigureServices(builder.Services);

        // Web API Services
        builder.Services.AddTransient<IHomeControllerService, HomeControllerService>();
        builder.Services.AddTransient<IRecipeControllerService, RecipeControllerService>();
        builder.Services.AddTransient<ICartControllerService, CartControllerService>();
        builder.Services.AddTransient<IAccountControllerService, AccountControllerService>();
        builder.Services.AddTransient<IOrderControllerService, OrderControllerService>();
    }
}
