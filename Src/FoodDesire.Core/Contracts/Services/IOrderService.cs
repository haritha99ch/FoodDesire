namespace FoodDesire.Core.Contracts.Services;
public interface IOrderService {
    Task<Order> NewOrder(Order order);
    Task<List<Order>> GetAllOrders();
    Task<List<Order>> GetRemainingOrders();
    Task<Order> GetOrderById(int orderId);
    Task<Order> UpdateOrder(Order order);
    Task<bool> DeleteOrderById(int orderId);
    Task<Order> GetPendingOrderForCustomer(int userId);
    Task<List<Order>> GetAllCustomerOrders(int customerId);
    Task<int> GetCompletedOrderCount();
    Task<int> GetPendingOrderCount();
}
