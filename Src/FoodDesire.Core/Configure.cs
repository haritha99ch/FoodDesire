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

        services.AddTransient<IAuthenticationService, AuthenticationService>();
        services.AddTransient<IAccountService, AccountService>();
        services.AddTransient<ICoreUserService, CoreUserService>();

        services.AddTransient<IAdminService, AdminService>();
        services.AddTransient<IChefService, ChefService>();
        services.AddTransient<IDelivererService, DelivererService>();
        services.AddTransient<ISupplierService, SupplierService>();
        services.AddTransient<IUserService<Admin>, UserService<Admin>>();
        services.AddTransient<IUserService<Chef>, UserService<Chef>>();
        services.AddTransient<IUserService<Supplier>, UserService<Supplier>>();
        services.AddTransient<IUserService<Deliverer>, UserService<Deliverer>>();
        services.AddTransient<IUserService<Customer>, UserService<Customer>>();

        services.AddTransient<IIngredientService, IngredientService>();
        services.AddTransient<IOrderDeliveryService, OrderDeliveryService>();
        services.AddTransient<ISupplyService, SupplyService>();

        services.AddTransient<IRecipeReviewService, RecipeReviewService>();
    }

    public static void ConfigureServices(IServiceCollection services) {
        GetServices(services);
    }
}
