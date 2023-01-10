using Microsoft.EntityFrameworkCore.Diagnostics;

namespace FoodDesire.DAL;
public static class Configure {
    private static readonly IgnoringIdentityResolutionInterceptor IgnoringIdentityResolutionInterceptor = new();

    public static void ConfigureServicesForIMS(
        IServiceCollection services,
        string dbConnectionString
        ) {

    }

    public static void ConfigureServicesForWebAPI(
        IServiceCollection services,
        string dbConnectionString
        ) {

    }

    public static void ConfigureServices(
        IServiceCollection services,
        string dbConnectionString
        ) {
        services.AddDbContext<FoodDesireContext>(
            options =>
                options.AddInterceptors(IgnoringIdentityResolutionInterceptor).UseSqlServer(dbConnectionString));

        services.AddScoped<IRepository<Account>, Repository<Account>>();
        services.AddScoped<ITrackingRepository<User>, ITrackingRepository<User>>();
        services.AddScoped<ITrackingRepository<Supply>, ITrackingRepository<Supply>>();
        services.AddScoped<ITrackingRepository<IngredientCategory>, ITrackingRepository<IngredientCategory>>();
        services.AddScoped<ITrackingRepository<Payment>, ITrackingRepository<Payment>>();
    }
}
