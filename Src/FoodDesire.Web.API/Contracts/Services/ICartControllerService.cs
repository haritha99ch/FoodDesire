namespace FoodDesire.Web.API.Contracts.Services;
public interface ICartControllerService {
    Task<Order> GetOrderAsync(int orderId);
    Task<Order> GetPendingOrderAsync(int customerId);
    Task<Order> CreateOrderAsync(Order order);
    Task<bool> CancelOrderAsync(int orderId);
    Task<Order> PayForOrderAsync(int orderId);
    Task<bool> RemoveFoodItem(int foodItemId);
}
