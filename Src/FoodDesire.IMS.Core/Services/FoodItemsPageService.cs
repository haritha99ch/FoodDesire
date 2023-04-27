namespace FoodDesire.IMS.Core.Services;
internal class FoodItemsPageService : IFoodItemsPageService {
    private readonly IFoodItemService _foodItemService;

    public FoodItemsPageService(IFoodItemService foodItemService) {
        _foodItemService = foodItemService;
    }

    public async Task<FoodItem> AddFootItemToPreparingList(int foodItemId, int chefId) => await _foodItemService.PrepareFoodItem(foodItemId, chefId);

    public async Task<FoodItem> CompletePreparingFoodItem(int foodItemId) => await _foodItemService.FoodItemPrepared(foodItemId);

    public async Task<List<FoodItem>> GetAcceptedFoodItemsAsync(int chefId) => await _foodItemService.GetAcceptedFoodItem(chefId);

    public async Task<List<FoodItem>> GetQueuedFoodItemsAsync() => await _foodItemService.GetQueuedFoodItems();
}
