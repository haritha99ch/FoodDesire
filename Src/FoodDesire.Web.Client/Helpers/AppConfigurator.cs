using FoodDesire.Web.Client.Services;
using Microsoft.AspNetCore.Components.Authorization;

namespace FoodDesire.Web.Client.Helpers;
internal static class AppConfigurator {

    internal static void ConfigureServices(IServiceCollection services) {
        services.AddTransient<IRecipePageService, RecipePageService>();
        services.AddSingleton(typeof(IComponentCommunicationService<>), typeof(ComponentCommunicationService<>));
        services.AddBlazoredLocalStorageAsSingleton();
        services.AddSingleton<IAuthenticationService, AuthenticationService>();
        services.AddScoped<AuthenticationStateProvider, UserAuthenticationStateProvider>();
        services.AddTransient<IAccountPageService, AccountPageService>();
    }
}
