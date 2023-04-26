namespace FoodDesire.Web.API.Services;
public class OrderControllerService : IOrderControllerService {
    private readonly IOrderService _orderService;

    public OrderControllerService(IOrderService orderService) {
        _orderService = orderService;
    }

    public async Task<Order> GetOrderByIdAsync(int orderId) => await _orderService.GetOrderById(orderId);

    public async Task<List<Order>> GetOrdersAsync(int customerId) => await _orderService.GetAllCustomerOrders(customerId);
}
