using FoodDesire.Core.Contracts.Services;
using FoodDesire.DAL.Contracts.Repositories;

namespace FoodDesire.Core;
public class FoodItemService: IFoodItemService {
    private readonly IRepository<FoodItem> _foodItemRepository;
    private readonly IRepository<Order> _orderRepository;

    public FoodItemService(
        IRepository<FoodItem> foodItemRepository,
        IRepository<Order> orderRepository
        ) {
        _foodItemRepository = foodItemRepository;
        _orderRepository = orderRepository;
    }

    public async Task<FoodItem> NewFoodItem(FoodItem foodItem) {
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
        Order order = await _orderRepository.GetByID(foodItem.OrderId);
        order.Status = OrderStatus.Preparing;
        await _orderRepository.Update(order);
        return foodItem;
    }

    public async Task<FoodItem> FoodItemPrepared(int foodItemId) {
        FoodItem foodItem = await _foodItemRepository.GetByID(foodItemId);
        foodItem.Status = FoodItemStatus.Prepared;
        foodItem.Deleted = true;
        foodItem = await _foodItemRepository.Update(foodItem);
        return foodItem;
    }

    public async Task<bool> RemoveFoodItem(int foodItemId) {
        bool foodItemDeleted = await _foodItemRepository.Delete(foodItemId);
        return foodItemDeleted;
    }

}
