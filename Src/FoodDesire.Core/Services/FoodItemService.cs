namespace FoodDesire.Core.Services;
public class FoodItemService: IFoodItemService {
    private readonly IRepository<FoodItem> _foodItemRepository;
    private readonly IRecipeService _recipeService;
    private readonly IRepository<Order> _orderRepository;

    public FoodItemService(
        IRepository<FoodItem> foodItemRepository,
        IRepository<Order> orderRepository,
        IRecipeService recipeService
        ) {
        _foodItemRepository = foodItemRepository;
        _orderRepository = orderRepository;
        _recipeService = recipeService;
    }

    public async Task<FoodItem> NewFoodItem(FoodItem foodItem) {
        Recipe recipe = await _recipeService.GetRecipeById(foodItem.RecipeId);
        recipe.RecipeIngredients.ForEach(e => {
            foodItem.FoodItemIngredients
                .Add(new FoodItemIngredient {
                    Recipe_Id = e.Recipe_Id,
                    Ingredient_Id = e.Ingredient_Id,
                    Amount = e.Amount,
                    IsRequired = e.IsRequired,
                    RecommendedMultiplier = e.RecommendedAmount / e.Amount,
                    PricePerMultiplier = e.PricePerMultiplier,
                    Multiplier = e.IsRequired ? 1 : 0
                });

        });
        FoodItem newFoodItem = await _foodItemRepository.Add(foodItem);
        foodItem = await UpdateFoodItem(newFoodItem);
        return newFoodItem;
    }

    public async Task<List<FoodItem>> GetAllFoodItemsForOrder(int orderId) {
        Order order = await _orderRepository.GetByID(orderId);
        List<FoodItem> foodItems = order.FoodItems!.ToList();
        return foodItems;
    }
    public async Task<FoodItem> UpdateFoodItem(FoodItem foodItem) {
        Recipe recipe = await _recipeService.GetRecipeById(foodItem.RecipeId);
        foodItem.Price = recipe.FixedPrice;
        foodItem.FoodItemIngredients
            .ForEach(e => {
                if(!e.CanModify) return;
                decimal multiplierPrice = Convert.ToDecimal(Convert.ToDouble(e.PricePerMultiplier) * e.Multiplier);
                if(!e.IsRequired) {
                    foodItem.Price += multiplierPrice;
                    return;
                }
                foodItem.Price += (e.Multiplier != 1) ? multiplierPrice - e.PricePerMultiplier : 0;


                //if(e.Multiplier != 1 && e.IsRequired)
                //    foodItem.Price += Convert.ToDecimal(Convert.ToDouble(e.PricePerMultiplier) * (e.Multiplier - 1));
                //if(e.IsRequired) return;
                //foodItem.Price += Convert.ToDecimal(Convert.ToDouble(e.PricePerMultiplier) * e.Multiplier);
            });
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
