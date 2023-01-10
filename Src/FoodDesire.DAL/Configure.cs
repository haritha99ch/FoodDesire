using Microsoft.EntityFrameworkCore.Diagnostics;

namespace FoodDesire.DAL;
public static class Configure {
    private static readonly IgnoringIdentityResolutionInterceptor IgnoringIdentityResolutionInterceptor = new();

    public static void ConfigureServicesForIMS(
        IServiceCollection services,
        string dbConnectionString
        ) {
        ConfigureServices(services, dbConnectionString);
        services.AddScoped<IRepository<Admin>, Repository<Admin>>();
        services.AddScoped<IRepository<Chef>, Repository<Chef>>();
        services.AddScoped<IRepository<Deliverer>, Repository<Deliverer>>();
        services.AddScoped<ITrackingRepository<Supply>, ITrackingRepository<Supply>>();
        services.AddScoped<ITrackingRepository<IngredientCategory>, ITrackingRepository<IngredientCategory>>();
    }

    public static void ConfigureServicesForWebAPI(
        IServiceCollection services,
        string dbConnectionString
        ) {
        ConfigureServices(services, dbConnectionString);
        services.AddScoped<IRepository<Customer>, Repository<Customer>>();
    }

    public static void ConfigureServices(
        IServiceCollection services,
        string dbConnectionString
        ) {
        services.AddDbContext<FoodDesireContext>(
            options =>
                options.AddInterceptors(IgnoringIdentityResolutionInterceptor).UseSqlServer(dbConnectionString));
        services.AddScoped<IRepository<Ingredient>, Repository<Ingredient>>();
        services.AddScoped<IRepository<FoodItem>, Repository<FoodItem>>();
        services.AddScoped<IRepository<Delivery>, Repository<Delivery>>();
        services.AddScoped<IRepository<Order>, Repository<Order>>();
        services.AddScoped<IRepository<Account>, Repository<Account>>();
        services.AddScoped<ITrackingRepository<Payment>, ITrackingRepository<Payment>>();
        services.AddScoped<ITrackingRepository<User>, ITrackingRepository<User>>();
        services.AddScoped<IRepository<RecipeCategory>, Repository<RecipeCategory>>();
        services.AddScoped<IRepository<RecipeIngredient>, Repository<RecipeIngredient>>();
        services.AddScoped<IRepository<Recipe>, Repository<Recipe>>();
    }
}
