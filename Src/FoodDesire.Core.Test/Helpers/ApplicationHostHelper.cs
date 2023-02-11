namespace FoodDesire.Core.Test.Helpers;
public static class ApplicationHostHelper {
    private static IHost? _host;
    //This helper method will register all the services using Dependency injection
    public static IHost ConfigureHost(string contextName) {
        _host = Host.CreateDefaultBuilder()
            .ConfigureServices((context, services) => {
                services.AddDbContext<FoodDesireContext>(options => {
                    DbContextHelper.ConfigureDbContextOptions(contextName, options);
                });
                services.AddScoped<FoodDesireContext>();
                DAL.Configure.ConfigureAllForTesting(services);
                Core.Configure.ConfigureAllForTesting(services);
            }).Build();
        return _host;
    }

    public static T GetService<T>() where T : class {
        if(!(_host!.Services.GetRequiredService(typeof(T)) is not T service)) return service;

        throw new ArgumentException($"{typeof(T)} needs to be registered in ConfigureServices within DAL.Configure or Core.Configure");
    }

    public static void TearDownHost() {
        _host!.Dispose();
    }
}