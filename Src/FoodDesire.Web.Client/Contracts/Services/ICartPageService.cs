namespace FoodDesire.Web.Client.Contracts.Services;
public interface ICartPageService {
    Task<Order> GetUserCurrentOrderAsync();
    Task<Order> GetOrderByIdAsync(int orderId);
    Task<Order> PayForOrderAsync(int orderId);
    Task<bool> CancelOrderAsync(int orderId);
    Task<bool> RemoveFoodItemByIdAsync(int foodItemId);
    Task<List<FoodItemListDetail>> GetFoodItemsForOrderAsync(int orderId);
}
