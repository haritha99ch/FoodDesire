namespace FoodDesire.IMS.Core.Contracts.Services;
public interface IFoodItemsPageService {
    Task<List<FoodItem>> GetQueuedFoodItemsAsync();
    Task<List<FoodItem>> GetAcceptedFoodItemsAsync(int chefId);
    Task<FoodItem> AddFootItemToPreparingList(int foodItemId, int chefId);
    Task<FoodItem> CompletePreparingFoodItem(int foodItemId);
}
