using FoodDesire.Web.Client.Services;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

namespace FoodDesire.Web.Client.Helpers;
internal static class AppConfigurator {
    internal static void Configure(WebAssemblyHostBuilder builder) { }

    internal static void ConfigureServices(IServiceCollection services) {
        services.AddTransient<IRecipePageService, RecipePageService>();
        services.AddSingleton(typeof(IComponentCommunicationService<>), typeof(ComponentCommunicationService<>));
        services.AddSingleton<IAuthenticationService, AuthenticationService>();
        services.AddTransient<IAccountPageService, AccountPageService>();
    }
}
