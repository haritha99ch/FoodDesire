namespace FoodDesire.Core.Contracts.Services;
public interface IOrderService {
    Task<Order> NewOrder(Order order);
    Task<List<Order>> GetAllOrders();
    Task<List<Order>> GetRemainingOrders();
    Task<Order> GetOrderById(int orderId);
    Task<bool> DeleteOrderById(int orderId);
    Task<Order> GetPendingOrderForCustomer(int userId);
}
