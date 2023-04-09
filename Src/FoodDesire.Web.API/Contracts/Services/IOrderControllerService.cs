namespace FoodDesire.Web.API.Contracts.Services;
public interface IOrderControllerService {
    Task<Order> GetOrderAsync(int orderId);
    Task<Order> GetPendingOrderAsync(int customerId);
    Task<Order> CreateOrderAsync(Order order);
    Task<bool> CancelOrderAsync(int orderId);
    Task<Order> PayForOrderAsync(int orderId);
}
