namespace FoodDesire.Web.API.Contracts.Services;
public interface IOrderControllerService {
    Task<List<Order>> GetOrdersAsync(int customerId);
    Task<Order> GetOrderByIdAsync(int orderId);
}
