namespace FoodDesire.Core;
public class FoodItemService: IFoodItemService {
    private readonly ITrackingRepository<FoodItem> _foodItemTrackingRepository;
    private readonly IRepository<FoodItem> _foodItemRepository;

    public FoodItemService(
        ITrackingRepository<FoodItem> foodItemTrackingRepository,
        IRepository<FoodItem> foodItemRepository
        ) {
        _foodItemTrackingRepository = foodItemTrackingRepository;
        _foodItemRepository = foodItemRepository;
    }

    public async Task<FoodItem> NewFoodItemFromRecipe(Recipe recipe, int orderId) {
        FoodItem foodItem = new FoodItem() {
            Id = orderId,
            Recipe = recipe,
        };
        foodItem = await _foodItemRepository.Add(foodItem);
        return foodItem;
    }

    public async Task<List<FoodItem>> GetAllFoodItemsForOrder(int orderId) {
        Expression<Func<FoodItem, bool>> filter = e => e.OrderId == orderId;
        Expression<Func<FoodItem, int>> order = e => e.OrderId;

        List<FoodItem> foodItems = await _foodItemRepository.Get(filter, order);
        return foodItems;
    }
    public async Task<FoodItem> UpdateFoodItem(FoodItem foodItem) {
        FoodItem updatedFoodItem = await _foodItemRepository.Update(foodItem);
        return updatedFoodItem;
    }

    public async Task<FoodItem> PrepareFoodItem(int foodItemId, int chefId) {
        FoodItem foodItem = await _foodItemRepository.GetByID(foodItemId);
        foodItem.ChefId = chefId;
        foodItem = await _foodItemRepository.Update(foodItem);
        return foodItem;
    }

    public async Task<bool> FoodItemPrepared(int foodItemId) {
        bool foodItemIsPrepared = await _foodItemTrackingRepository.SoftDelete(foodItemId); //Soft deleting means the food item has been prepared
        await _foodItemTrackingRepository.SaveChanges();
        return foodItemIsPrepared;
    }

    public async Task<bool> RemoveFoodItem(int foodItemId) {
        bool foodItemDeleted = await _foodItemRepository.Delete(foodItemId);
        return foodItemDeleted;
    }

}
