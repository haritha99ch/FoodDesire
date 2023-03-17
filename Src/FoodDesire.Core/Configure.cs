using FoodDesire.Core.Services;
using Microsoft.Extensions.DependencyInjection;

namespace FoodDesire.Core;
public static class Configure {

    public static void ConfigureAllForTesting(IServiceCollection services) {
        GetServices(services);
    }

    private static void GetServices(IServiceCollection services) {
        services.AddTransient<IOrderService, OrderService>();
        services.AddTransient<IFoodItemService, FoodItemService>();
        services.AddTransient<IPaymentService, PaymentService>();
        services.AddTransient<IRecipeService, RecipeService>();

        services.AddTransient<ICustomerService, CustomerService>();
        services.AddTransient<ICustomerOrderService, CustomerOrderService>();

        services.AddTransient<IAdminService, AdminService>();
        services.AddTransient<IChefService, ChefService>();
        services.AddTransient<IDelivererService, DelivererService>();
        services.AddTransient<ISupplierService, SupplierService>();
        services.AddTransient<IIngredientService, IngredientService>();
        services.AddTransient<IOrderDeliveryService, OrderDeliveryService>();
        services.AddTransient<ISupplyService, SupplyService>();
    }

    public static void ConfigureServices(IServiceCollection services) {
        GetServices(services);
    }
}
