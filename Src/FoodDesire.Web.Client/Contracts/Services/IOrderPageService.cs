namespace FoodDesire.Web.Client.Contracts.Services;
public interface IOrderPageService {
    Task<List<Order>> GetOrdersAsync();
    Task<Order> GetAsync(int orderId);
}
