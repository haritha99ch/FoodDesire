using Microsoft.Extensions.DependencyInjection;

namespace FoodDesire.Core;
public static class Configure {

    public static void ConfigureServicesForIMS(IServiceCollection services) {
        ConfigureServices(services);
        services.AddTransient<IAdminService, AdminService>();
        services.AddTransient<IChefService, ChefService>();
        services.AddTransient<IDelivererService, DelivererService>();
        services.AddTransient<ISupplierService, SupplierService>();
        services.AddTransient<IIngredientService, IngredientService>();
        services.AddTransient<IOrderDeliveryService, OrderDeliveryService>();
    }

    public static void ConfigureServicesForWebAPI(IServiceCollection services) {
        ConfigureServices(services);
        services.AddTransient<ICustomerService, CustomerService>();
        services.AddTransient<ICustomerOrderService, CustomerOrderService>();
    }

    public static void ConfigureAllForTesting(IServiceCollection services) {
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
    }

    private static void ConfigureServices(IServiceCollection services) {
        services.AddTransient<IOrderService, OrderService>();
        services.AddTransient<IFoodItemService, FoodItemService>();
        services.AddTransient<IPaymentService, PaymentService>();
        services.AddTransient<IRecipeService, RecipeService>();
    }
}
