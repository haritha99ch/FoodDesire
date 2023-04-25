namespace FoodDesire.Core.Test;
[TestFixture]
public class FoodOrderServices : Services {
    public FoodOrderServices() : base("FoodOrderServices") { }

    [OneTimeSetUp]
    public async Task Setup() {
        await _context.Database.EnsureDeletedAsync();
        await _context.Database.EnsureCreatedAsync();

        await _recipeService.NewRecipeCategory(RecipeDataHelper.GetRecipeCategoryPayload());
        await _ingredientService.NewIngredientCategory(IngredientDataHelper.GetIngredientCategoryPayload());
        await _chefService.CreateAccount(UserDataHelper.GetChefPayload());
        await _customerService.CreateAccount(UserDataHelper.GetCustomerPayload());
        foreach (var ingredient in RecipeDataHelper.GetIngredients()) {
            await _ingredientService.NewIngredient(ingredient);
        }
        foreach (var recipe in RecipeDataHelper.GetRecipes()) {
            await _recipeService.NewRecipe(recipe);
        }
    }

    [OneTimeTearDown]
    public async Task TearDown() {
        await _context.Database.EnsureDeletedAsync();
        ApplicationHostHelper.TearDownHost();
    }

    [Test, Order(1)]
    public async Task NewOrder() {
        Order order = new Order {
            CustomerId = 1,
        };
        order = await _orderService.NewOrder(order);
        List<Order> orders = await _orderService.GetAllOrders();

        Assert.That(order, Is.Not.Null);
        Assert.That(orders[0], Is.Not.Null);
    }

    [Test, Order(2)]
    public async Task NewFoodItem() {
        FoodItem foodItem = new FoodItem {
            RecipeId = 2,
            OrderId = 1,
        };
        foodItem = await _foodItemService.NewFoodItem(foodItem);
        Assert.That(foodItem.Price, Is.EqualTo(foodItem.Recipe!.FixedPrice));
    }

    [Test, Order(3)]
    public async Task AddNewFoodItemToOrder() {
        FoodItem foodItem = new FoodItem {
            RecipeId = 1,
            OrderId = 1,
        };
        await _foodItemService.NewFoodItem(foodItem);
        List<FoodItem>? foodItems = await _foodItemService.GetAllFoodItemsForOrder(1);
        Assert.That(foodItems, Has.Count.EqualTo(2));
    }

    [Test, Order(4)]
    public async Task ModifyFoodItem() {
        List<FoodItem>? foodItems = await _foodItemService.GetAllFoodItemsForOrder(1);

        FoodItem? selectedFoodItem = foodItems[0];
        selectedFoodItem.FoodItemIngredients[0].Multiplier = 2;
        selectedFoodItem.FoodItemIngredients[1].Multiplier = 0.5;
        selectedFoodItem.FoodItemIngredients[2].Multiplier = 2;

        FoodItem updatedFoodItem = await _foodItemService.UpdateFoodItem(selectedFoodItem);
        Assert.That(updatedFoodItem.Price, Is.EqualTo(3445.15));
    }

    [Test, Order(5)]
    public async Task AddOptionalIngredientToFoodItem() {
        List<FoodItem>? foodItems = await _foodItemService.GetAllFoodItemsForOrder(1);

        FoodItem selectedFoodItem = foodItems[1];
        selectedFoodItem.FoodItemIngredients[4].Multiplier = 2;

        FoodItem updatedFoodItem = await _foodItemService.UpdateFoodItem(selectedFoodItem);
        Assert.That(updatedFoodItem.Price, Is.EqualTo(328.18));
    }

    [Test, Order(6)]
    public async Task PrepareFoodItem() {
        // Food item is being prepared by chef with id 1
        FoodItem foodItem = await _foodItemService.PrepareFoodItem(1, 1);
        foodItem = await _foodItemService.GetFoodItemById(1);
        Assert.That(foodItem.Status, Is.EqualTo(FoodItemStatus.Preparing));
    }

    [Test, Order(7)]
    public async Task PreparedFoodItem() {
        FoodItem foodItem = await _foodItemService.FoodItemPrepared(1);
        foodItem = await _foodItemService.GetFoodItemById(1);
        Assert.That(foodItem.Status, Is.EqualTo(FoodItemStatus.Prepared));
    }

    [Test, Order(8)]
    public async Task OrderIsPrepared() {
        FoodItem foodItem = await _foodItemService.FoodItemPrepared(2);

        Order order = await _orderService.GetOrderById(1);
        Assert.That(order.Status, Is.EqualTo(OrderStatus.Prepared));
    }

    [Test, Order(9)]
    public async Task GetPendingOrders() {
        List<Order> orders = await _orderService.GetRemainingOrders();

        Assert.That(orders.Count, Is.EqualTo(1));
    }

    [Test, Order(10)]
    public async Task RemoveAFoodItem() {
        bool removed = await _foodItemService.RemoveFoodItem(1);

        FoodItem foodItem = await _foodItemService.GetFoodItemById(1);
        Assert.That(foodItem, Is.Null);
    }

    [Test, Order(11)]
    public async Task RemoveAnOrder() {
        bool removed = await _orderService.DeleteOrderById(1);

        Order order = await _orderService.GetOrderById(1);
        Assert.That(order, Is.Null);
    }


}
