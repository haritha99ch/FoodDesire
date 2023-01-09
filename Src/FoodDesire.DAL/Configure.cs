using Microsoft.EntityFrameworkCore.Diagnostics;

namespace FoodDesire.DAL;
public static class Configure {
    private static readonly IgnoringIdentityResolutionInterceptor IgnoringIdentityResolutionInterceptor = new();
    public static void ConfigureServices(
        IServiceCollection services, string dbConnectionString
        ) {
        services.AddDbContext<FoodDesireContext>(
            options => options.AddInterceptors(IgnoringIdentityResolutionInterceptor).UseSqlServer(dbConnectionString)
            );
    }
}
