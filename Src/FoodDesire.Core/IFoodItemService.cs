namespace FoodDesire.Core;
public interface IFoodItemService {
    Task<FoodItem> NewFoodItemForOrder(FoodItem foodItem);
    Task<bool> RemoveFoodItem(int foodItemId);
    Task<FoodItem> UpdateFoodItem(FoodItem foodItem);
    Task<FoodItem> PrepareFoodItem(int foodItemId, int chefId);
    Task<bool> FoodItemPrepared(int foodItemId);
}
