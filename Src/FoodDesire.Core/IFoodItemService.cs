namespace FoodDesire.Core;
public interface IFoodItemService {
    Task<FoodItem> NewFoodItemFromRecipe(Recipe recipe, int orderId);
    Task<bool> RemoveFoodItem(int foodItemId);
    Task<FoodItem> UpdateFoodItem(FoodItem foodItem);
    Task<List<FoodItem>> GetAllFoodItemsForOrder(int orderId);
    Task<FoodItem> PrepareFoodItem(int foodItemId, int chefId);
    Task<bool> FoodItemPrepared(int foodItemId);
}
