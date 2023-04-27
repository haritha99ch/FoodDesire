﻿namespace FoodDesire.Core.Services;
public class FoodItemService : IFoodItemService {
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
                    Recipe_Name = e.Recipe_Name,
                    Ingredient_Id = e.Ingredient_Id,
                    Ingredient_Name = e.Ingredient_Name,
                    Amount = e.Amount,
                    IsRequired = e.IsRequired,
                    CanModify = e.CanModify,
                    RecommendedMultiplier = e.RecommendedAmount / e.Amount,
                    PricePerMultiplier = e.PricePerMultiplier,
                    Multiplier = e.IsRequired ? 1 : 0
                });

        });
        FoodItem newFoodItem = await _foodItemRepository.Add(foodItem);
        newFoodItem.Order!.Status = OrderStatus.Pending;
        foodItem = await UpdateFoodItem(newFoodItem);
        return newFoodItem;
    }

    public async Task<List<FoodItem>> GetAllFoodItemsForOrder(int orderId) {
        Order order = await _orderRepository.GetByID(orderId);
        Expression<Func<FoodItem, bool>> filterExpression = e => e.OrderId == order.Id;

        IQueryable<FoodItem> filter(IQueryable<FoodItem> e) => e.Where(filterExpression);
        IIncludableQueryable<FoodItem, object> include(IQueryable<FoodItem> e) =>
            e.Include(e => e.Recipe).ThenInclude(e => e!.Images.Take(1));

        List<FoodItem>? foodItems = await _foodItemRepository.Get(filter: filter, include: include);
        return foodItems;
    }
    public async Task<FoodItem> UpdateFoodItem(FoodItem foodItem) {
        Recipe recipe = await _recipeService.GetRecipeById(foodItem.RecipeId);
        foodItem.Price = recipe.FixedPrice;
        foreach (FoodItemIngredient e in foodItem.FoodItemIngredients) {
            if (!e.CanModify) continue;
            decimal multiplierPrice = Convert.ToDecimal(Convert.ToDouble(e.PricePerMultiplier) * e.Multiplier);
            if (!e.IsRequired) {
                foodItem.Price += multiplierPrice;
                continue;
            }
            foodItem.Price += (e.Multiplier != 1) ? multiplierPrice - e.PricePerMultiplier : 0;
        }
        foodItem = await _foodItemRepository.Update(foodItem);
        Order order = await _orderRepository.GetByID(foodItem.OrderId);
        List<FoodItem>? foodItems = await GetAllFoodItemsForOrder(order.Id);
        order!.Price = foodItems.Sum(e => e.Price * e.Quantity);
        FoodItem updatedFoodItem = await _foodItemRepository.Update(foodItem);
        return updatedFoodItem;
    }

    public async Task<FoodItem> PrepareFoodItem(int foodItemId, int chefId) {
        FoodItem foodItem = await _foodItemRepository.GetByID(foodItemId);
        foodItem.ChefId = chefId;
        foodItem.Status = FoodItemStatus.Preparing;
        foodItem = await _foodItemRepository.Update(foodItem);
        return foodItem;
    }

    public async Task<FoodItem> FoodItemPrepared(int foodItemId) {
        FoodItem foodItem = await GetFoodItemById(foodItemId);
        foodItem.Status = FoodItemStatus.Prepared;
        foodItem.Deleted = true;
        foodItem = await _foodItemRepository.Update(foodItem);

        Order order = await _orderRepository.GetByID(foodItem.OrderId);

        List<FoodItem>? foodItems = await GetAllFoodItemsForOrder(order.Id);
        order.Status = OrderStatus.Prepared;
        foodItems?.ForEach(e => {
            if (e.Status == FoodItemStatus.Preparing) order.Status = OrderStatus.Preparing;
        });
        await _orderRepository.Update(order);

        return foodItem;
    }

    public async Task<bool> RemoveFoodItem(int foodItemId) {
        FoodItem foodItem = await _foodItemRepository.GetByID(foodItemId);
        Order order = await _orderRepository.GetByID(foodItem.OrderId);
        bool foodItemDeleted = await _foodItemRepository.Delete(foodItemId);

        List<FoodItem>? foodItems = await GetAllFoodItemsForOrder(order.Id);
        if (foodItems.Count == 0) return await _orderRepository.Delete(order.Id);
        order!.Price = foodItems.Sum(e => e.Price * e.Quantity);

        order = await _orderRepository.Update(order);
        return foodItemDeleted;
    }

    public async Task<FoodItem> GetFoodItemById(int foodItemId) {
        FoodItem foodItem = await _foodItemRepository.GetByID(foodItemId);
        return foodItem;
    }

    public async Task<List<FoodItem>> GetQueuedFoodItems() {
        Expression<Func<FoodItem, bool>> filterExpression = e => e.Status.Equals(FoodItemStatus.Queued);

        IQueryable<FoodItem> filter(IQueryable<FoodItem> e) => e.Where(filterExpression);
        IIncludableQueryable<FoodItem, object> include(IQueryable<FoodItem> e) =>
            e.Include(e => e.Recipe).Include(e => e.Chef).ThenInclude(e => e!.User).Include(e => e.Order)!;
        IOrderedQueryable<FoodItem> order(IQueryable<FoodItem> e) => e.OrderBy(e => e.OrderId).OrderBy(e => e.Order!.DateTime);

        List<FoodItem>? foodItems = await _foodItemRepository.Get(filter, order, include);
        return foodItems;
    }

    public async Task<List<FoodItem>> GetAcceptedFoodItem(int chefId) {
        Expression<Func<FoodItem, bool>> filterExpression = e => e.ChefId == chefId && !e.Status.Equals(FoodItemStatus.Prepared);

        IQueryable<FoodItem> filter(IQueryable<FoodItem> e) => e.Where(filterExpression);
        IIncludableQueryable<FoodItem, object> include(IQueryable<FoodItem> e) =>
            e.Include(e => e.Recipe).Include(e => e.Chef).ThenInclude(e => e!.User).Include(e => e.Order)!;
        IOrderedQueryable<FoodItem> order(IQueryable<FoodItem> e) => e.OrderBy(e => e.OrderId).OrderBy(e => e.Order!.DateTime);

        List<FoodItem>? foodItems = await _foodItemRepository.Get(filter, order, include);
        return foodItems;
    }
}
