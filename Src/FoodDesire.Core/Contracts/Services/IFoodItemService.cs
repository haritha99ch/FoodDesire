namespace FoodDesire.Core.Contracts.Services;
public interface IFoodItemService {
    Task<FoodItem> NewFoodItem(FoodItem foodItem);
    Task<FoodItem> GetFoodItemById(int foodItemId);
    Task<bool> RemoveFoodItem(int foodItemId);
    Task<FoodItem> UpdateFoodItem(FoodItem foodItem);
    Task<FoodItem> PrepareFoodItem(int foodItemId, int chefId);
    Task<FoodItem> FoodItemPrepared(int foodItemId);
}
