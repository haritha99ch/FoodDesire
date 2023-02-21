namespace FoodDesire.Core.Test;
[TestFixture]
public class CustomerOrderServices : Services {
    public CustomerOrderServices() : base("CustomerOrderServices") { }

    [OneTimeSetUp]
    public async Task Setup() {
        await _context.Database.EnsureDeletedAsync();
        await _context.Database.EnsureCreatedAsync();

        await _ingredientService.NewIngredientCategory(IngredientDataHelper.GetIngredientCategoryPayload());
        await _recipeService.NewRecipeCategory(RecipeDataHelper.GetRecipeCategoryPayload());
        await _chefService.CreateAccount(UserDataHelper.GetChefPayload());

        foreach (var ingredient in RecipeDataHelper.GetIngredients()) {
            await _ingredientService.NewIngredient(ingredient);
        }
        foreach (var recipe in RecipeDataHelper.GetRecipes()) {
            await _recipeService.NewRecipe(recipe);
        }
        await _foodItemService.NewFoodItem(new FoodItem() {
            RecipeId = 1,
            Order = new Order() {
                Customer = UserDataHelper.GetCustomerPayload()
            }
        });

    }

    [OneTimeTearDown]
    public async Task TearDown() {
        await _context.Database.EnsureDeletedAsync();
        ApplicationHostHelper.TearDownHost();
    }

    [Test, Order(1)]
    public async Task GetOrdersForCustomer() {
        List<Order> orders = await _customerOrderService.GetAllOrdersForCustomerById(1);
        Assert.That(orders.Count, Is.EqualTo(1));
    }

    [Test, Order(2)]
    public async Task GetAllPendingOrders() {
        List<Order> orders = await _customerOrderService.GetAllPendingOrdersForCustomerById(1);
        Assert.That(orders.Count, Is.EqualTo(1));
    }

    [Test, Order(3)]
    public async Task GetAllOrdersToDeliver() {
        await _foodItemService.FoodItemPrepared(1);
        await _foodItemService.NewFoodItem(new FoodItem() {
            RecipeId = 2,
            OrderId = 1
        });

        List<Order> orders = await _customerOrderService.GetAllOrdersToDeliverForCustomerById(1);
        Assert.That(orders.Count, Is.EqualTo(0));

        await _foodItemService.FoodItemPrepared(2);
        orders = await _customerOrderService.GetAllOrdersToDeliverForCustomerById(1);
        Assert.That(orders.Count, Is.EqualTo(1));
    }
}
