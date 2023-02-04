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
        services.AddScoped<IRepository<Supplier>, Repository<Supplier>>();
        services.AddScoped<ITrackingRepository<Supply>, TrackingRepository<Supply>>();
        services.AddScoped<ITrackingRepository<IngredientCategory>, TrackingRepository<IngredientCategory>>();
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
        services.AddScoped<ITrackingRepository<Payment>, TrackingRepository<Payment>>();
        services.AddScoped<ITrackingRepository<User>, TrackingRepository<User>>();
        services.AddScoped<IRepository<RecipeCategory>, Repository<RecipeCategory>>();
        services.AddScoped<IRepository<RecipeIngredient>, Repository<RecipeIngredient>>();
        services.AddScoped<IRepository<Recipe>, Repository<Recipe>>();
    }

    public static void ConfigureAllForTesting(IServiceCollection services) {
        services.AddScoped<IRepository<Ingredient>, Repository<Ingredient>>();
        services.AddScoped<IRepository<FoodItem>, Repository<FoodItem>>();
        services.AddScoped<IRepository<Delivery>, Repository<Delivery>>();
        services.AddScoped<IRepository<Order>, Repository<Order>>();
        services.AddScoped<IRepository<Account>, Repository<Account>>();
        services.AddScoped<ITrackingRepository<Payment>, TrackingRepository<Payment>>();
        services.AddScoped<ITrackingRepository<User>, TrackingRepository<User>>();
        services.AddScoped<IRepository<RecipeCategory>, Repository<RecipeCategory>>();
        services.AddScoped<IRepository<RecipeIngredient>, Repository<RecipeIngredient>>();
        services.AddScoped<IRepository<Recipe>, Repository<Recipe>>();
        services.AddScoped<IRepository<Customer>, Repository<Customer>>();
        services.AddScoped<IRepository<Admin>, Repository<Admin>>();
        services.AddScoped<IRepository<Chef>, Repository<Chef>>();
        services.AddScoped<IRepository<Deliverer>, Repository<Deliverer>>();
        services.AddScoped<IRepository<Supplier>, Repository<Supplier>>();
        services.AddScoped<ITrackingRepository<Supply>, TrackingRepository<Supply>>();
        services.AddScoped<ITrackingRepository<IngredientCategory>, TrackingRepository<IngredientCategory>>();
    }
}
