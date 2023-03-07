using FoodDesire.DAL.Repositories;

namespace FoodDesire.DAL;
public static class Configure {

    public static void ConfigureServices(
        IServiceCollection services,
        string dbConnectionString
        ) {
        services.AddDbContext<FoodDesireContext>(
            options =>
                options.UseSqlServer(dbConnectionString), ServiceLifetime.Transient, ServiceLifetime.Transient);
        GetServices(services);
    }

    public static void ConfigureAllForTesting(IServiceCollection services) {
        GetServices(services);
    }

    private static void GetServices(IServiceCollection services) {
        services.AddTransient<IRepository<Ingredient>, Repository<Ingredient>>();
        services.AddTransient<IRepository<FoodItem>, Repository<FoodItem>>();
        services.AddTransient<IRepository<Delivery>, Repository<Delivery>>();
        services.AddTransient<IRepository<Order>, Repository<Order>>();
        services.AddTransient<IRepository<Account>, Repository<Account>>();
        services.AddTransient<ITrackingRepository<Payment>, TrackingRepository<Payment>>();
        services.AddTransient<ITrackingRepository<User>, TrackingRepository<User>>();
        services.AddTransient<IRepository<RecipeCategory>, Repository<RecipeCategory>>();
        services.AddTransient<IRepository<Recipe>, Repository<Recipe>>();
        services.AddTransient<IRepository<Customer>, Repository<Customer>>();
        services.AddTransient<IRepository<Admin>, Repository<Admin>>();
        services.AddTransient<IRepository<Chef>, Repository<Chef>>();
        services.AddTransient<IRepository<Deliverer>, Repository<Deliverer>>();
        services.AddTransient<IRepository<Supplier>, Repository<Supplier>>();
        services.AddTransient<ITrackingRepository<Supply>, TrackingRepository<Supply>>();
        services.AddTransient<ITrackingRepository<IngredientCategory>, TrackingRepository<IngredientCategory>>();
    }
}
