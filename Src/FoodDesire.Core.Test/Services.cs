namespace FoodDesire.Core.Test;
public abstract class Services {
    internal readonly IAdminService _adminService;
    internal readonly IChefService _chefService;
    internal readonly ISupplierService _supplierService;
    internal readonly ICustomerService _customerService;
    internal readonly IDelivererService _delivererService;
    internal readonly IIngredientService _ingredientService;
    internal readonly IRecipeService _recipeService;
    internal readonly IFoodItemService _foodItemService;
    internal readonly IOrderService _orderService;
    internal readonly ICustomerOrderService _customerOrderService;
    internal readonly IOrderDeliveryService _orderDeliveryServices;
    internal readonly IPaymentService _paymentService;
    internal readonly ApplicationDbContext _context;

    public Services(string hostName) {
        ApplicationHostHelper.ConfigureHost(hostName);

        _adminService = ApplicationHostHelper.GetService<IAdminService>();
        _chefService = ApplicationHostHelper.GetService<IChefService>();
        _supplierService = ApplicationHostHelper.GetService<ISupplierService>();
        _customerService = ApplicationHostHelper.GetService<ICustomerService>();
        _delivererService = ApplicationHostHelper.GetService<IDelivererService>();
        _ingredientService = ApplicationHostHelper.GetService<IIngredientService>();
        _recipeService = ApplicationHostHelper.GetService<IRecipeService>();
        _foodItemService = ApplicationHostHelper.GetService<IFoodItemService>();
        _orderService = ApplicationHostHelper.GetService<IOrderService>();
        _customerOrderService = ApplicationHostHelper.GetService<ICustomerOrderService>();
        _orderDeliveryServices = ApplicationHostHelper.GetService<IOrderDeliveryService>();
        _paymentService = ApplicationHostHelper.GetService<IPaymentService>();
        _context = ApplicationHostHelper.GetService<ApplicationDbContext>();
    }
}
