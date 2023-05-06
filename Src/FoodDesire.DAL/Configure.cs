using FoodDesire.DAL.Repositories;

namespace FoodDesire.DAL;
public static class Configure {

    public static void ConfigureServices(
        IServiceCollection services,
        string dbConnectionString
        ) {
        services.AddDbContext<ApplicationDbContext>(options
            => options.UseSqlServer(dbConnectionString), ServiceLifetime.Scoped, ServiceLifetime.Scoped);

        GetServices(services);
    }

    public static void ConfigureAllForTesting(IServiceCollection services) {
        GetServices(services);
    }

    private static void GetServices(IServiceCollection services) {
        services.AddScoped<IRepository<Ingredient>, Repository<Ingredient>>();
        services.AddScoped<IRepository<FoodItem>, Repository<FoodItem>>();
        services.AddScoped<IRepository<Delivery>, Repository<Delivery>>();
        services.AddScoped<IRepository<Order>, Repository<Order>>();
        services.AddScoped<IRepository<Account>, Repository<Account>>();
        services.AddScoped<ITrackingRepository<Payment>, TrackingRepository<Payment>>();
        services.AddScoped<ITrackingRepository<User>, TrackingRepository<User>>();
        services.AddScoped<IRepository<User>, Repository<User>>();
        services.AddScoped<IRepository<RecipeCategory>, Repository<RecipeCategory>>();
        services.AddScoped<IRepository<Recipe>, Repository<Recipe>>();
        services.AddScoped<IRepository<RecipeReview>, Repository<RecipeReview>>();
        services.AddScoped<IRepository<Customer>, Repository<Customer>>();
        services.AddScoped<IRepository<Admin>, Repository<Admin>>();
        services.AddScoped<IRepository<Chef>, Repository<Chef>>();
        services.AddScoped<IRepository<Deliverer>, Repository<Deliverer>>();
        services.AddScoped<IRepository<Supplier>, Repository<Supplier>>();
        services.AddScoped<IRepository<Account>, Repository<Account>>();
        services.AddScoped<IRepository<Supply>, Repository<Supply>>();
        services.AddScoped<ITrackingRepository<Supply>, TrackingRepository<Supply>>();
        services.AddScoped<ITrackingRepository<IngredientCategory>, TrackingRepository<IngredientCategory>>();
    }
}
