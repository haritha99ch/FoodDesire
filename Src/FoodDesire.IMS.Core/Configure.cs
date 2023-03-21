using FoodDesire.IMS.Core.Services;
using Microsoft.Extensions.DependencyInjection;

namespace FoodDesire.IMS.Core;
public static class Configure {
    public static void ConfigureServices(IServiceCollection services, string connectionString) {
        DAL.Configure.ConfigureServices(services, connectionString);
        FoodDesire.Core.Configure.ConfigureServices(services);

        services.AddSingleton<IFileService, FileService>();

        services.AddTransient<IHomeService, HomeService>();
        services.AddTransient<IIngredientsPageService, IngredientsPageService>();
        services.AddTransient<ISuppliesPageService, SuppliesPageService>();
        services.AddTransient<IEmployeePageService, EmployeePageService>();
    }
}
