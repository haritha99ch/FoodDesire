namespace FoodDesire.Core;
public class FoodItemService: IFoodItemService {
    private readonly ITrackingRepository<FoodItem> _foodItemTrackingRepository;
    private readonly IRepository<FoodItem> _foodItemRepository;
    private readonly IRepository<Order> _orderRepository;

    public FoodItemService(
        ITrackingRepository<FoodItem> foodItemTrackingRepository,
        IRepository<FoodItem> foodItemRepository
,
        IRepository<Order> orderRepository) {
        _foodItemTrackingRepository = foodItemTrackingRepository;
        _foodItemRepository = foodItemRepository;
        _orderRepository = orderRepository;
    }

    public async Task<FoodItem> NewFoodItemForOrder(FoodItem foodItem) {
        FoodItem newFoodItem = await _foodItemRepository.Add(foodItem);
        return newFoodItem;
    }

    public async Task<List<FoodItem>> GetAllFoodItemsForOrder(int orderId) {
        Order order = await _orderRepository.GetByID(orderId);
        List<FoodItem> foodItems = order.FoodItems!.ToList();
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
