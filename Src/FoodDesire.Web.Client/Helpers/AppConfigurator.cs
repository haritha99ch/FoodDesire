using FoodDesire.Web.Client.Contracts.Services;
using FoodDesire.Web.Client.Services;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

namespace FoodDesire.Web.Client.Helpers;
internal static class AppConfigurator {
    internal static void Configure(WebAssemblyHostBuilder builder) {
        string environmentName = builder.HostEnvironment.Environment;
        AppSettings.Configure.ConfigureEnvironment(builder.Configuration, environmentName);
    }

    internal static void ConfigureServices(IServiceCollection services) {
        services.AddTransient<IRecipePageService, RecipePageService>();
    }
}
