namespace FoodDesire.Web.API.Services;
public class CartControllerService : ICartControllerService {
    private readonly IOrderService _orderService;
    private readonly IPaymentService _paymentService;
    private readonly IFoodItemService _foodItemService;

    public CartControllerService(
        IOrderService orderService,
        IPaymentService paymentService,
        IFoodItemService foodItemService
        ) {
        _orderService = orderService;
        _paymentService = paymentService;
        _foodItemService = foodItemService;
    }

    public async Task<bool> CancelOrderAsync(int orderId) => await _orderService.DeleteOrderById(orderId);

    public async Task<Order> CreateOrderAsync(Order order) => await _orderService.NewOrder(order);

    public async Task<Order> GetOrderAsync(int orderId) => await _orderService.GetOrderById(orderId);

    public async Task<Order> GetPendingOrderAsync(int customerId) => await _orderService.GetPendingOrderForCustomer(customerId);

    public async Task<Order> PayForOrderAsync(int orderId) => await _paymentService.PaymentForOrder(orderId);

    public async Task<bool> RemoveFoodItem(int foodItemId) => await _foodItemService.RemoveFoodItem(foodItemId);
}
