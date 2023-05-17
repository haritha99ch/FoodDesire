namespace FoodDesire.Web.Client.Contracts.Services;
public interface ICartPageService {
    Task<Order> GetUserCurrentOrderAsync();
    Task<Order> GetOrderByIdAsync(int orderId);
    Task<Order> UpdateOrderAsync(Order order);
    Task<string> PayForOrderAsync(int orderId);
    Task<bool> CancelOrderAsync(int orderId);
    Task<bool> RemoveFoodItemByIdAsync(int foodItemId);
    Task<List<FoodItemListItem>> GetFoodItemsForOrderAsync(int orderId);
    Task<Order> CompletePayment(Order order);
}
