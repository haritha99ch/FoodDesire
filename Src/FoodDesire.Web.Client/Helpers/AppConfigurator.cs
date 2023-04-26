using FoodDesire.Web.Client.Services;
using Microsoft.AspNetCore.Components.Authorization;

namespace FoodDesire.Web.Client.Helpers;
internal static class AppConfigurator {

    internal static void ConfigureServices(IServiceCollection services) {
        MapperConfiguration? configuration = new(DtoConfigurator.Configure);
        IMapper? mapper = configuration.CreateMapper();
        services.AddSingleton(mapper);

        services.AddTransient<IHomePageService, HomePageService>();
        services.AddTransient<IRecipePageService, RecipePageService>();
        services.AddTransient<ICartPageService, CartPageService>();
        services.AddSingleton(typeof(IComponentCommunicationService<>), typeof(ComponentCommunicationService<>));
        services.AddBlazoredLocalStorageAsSingleton();
        services.AddSingleton<IAuthenticationService, AuthenticationService>();
        services.AddScoped<AuthenticationStateProvider, UserAuthenticationStateProvider>();
        services.AddTransient<IAccountPageService, AccountPageService>();
        services.AddTransient<IOrderPageService, OrderPageService>();
    }
}
