using FoodDesire.Web.Client.Services;

namespace FoodDesire.Web.Client.Helpers;
internal static class AppConfigurator {

    internal static void ConfigureServices(IServiceCollection services) {
        services.AddTransient<IRecipePageService, RecipePageService>();
        services.AddSingleton(typeof(IComponentCommunicationService<>), typeof(ComponentCommunicationService<>));
        services.AddBlazoredLocalStorageAsSingleton();
        services.AddSingleton<IAuthenticationService, AuthenticationService>();
        services.AddTransient<IAccountPageService, AccountPageService>();
    }
}
