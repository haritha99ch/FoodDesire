namespace FoodDesire.DAL;
public static class Configure {
    public static void ConfigureServices(
        IServiceCollection services, string dbConnectionString
        ) {
        services.AddDbContext<FoodDesireContext>(
            options => options.UseSqlServer(dbConnectionString)
            );
    }
}
