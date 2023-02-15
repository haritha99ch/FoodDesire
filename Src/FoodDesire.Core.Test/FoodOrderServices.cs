namespace FoodDesire.Core.Test;
[TestFixture]
public class FoodOrderServices {
    private readonly IFoodItemService _foodItemService;
    private readonly IIngredientService _ingredientService;
    private readonly IRecipeService _recipeService;
    private readonly IChefService _chefService;
    private readonly ICustomerService _customerService;
    private readonly IOrderService _orderService;
    private readonly FoodDesireContext _context;
    public FoodOrderServices() {
        ApplicationHostHelper.ConfigureHost("FoodOrderServices");

        _foodItemService = ApplicationHostHelper.GetService<IFoodItemService>();
        _ingredientService = ApplicationHostHelper.GetService<IIngredientService>();
        _recipeService = ApplicationHostHelper.GetService<IRecipeService>();
        _chefService = ApplicationHostHelper.GetService<IChefService>();
        _customerService = ApplicationHostHelper.GetService<ICustomerService>();
        _orderService = ApplicationHostHelper.GetService<IOrderService>();
        _context = ApplicationHostHelper.GetService<FoodDesireContext>();
    }

    [OneTimeSetUp]
    public async Task Setup() {
        await _context.Database.EnsureDeletedAsync();
        await _context.Database.EnsureCreatedAsync();

        await _recipeService.NewRecipeCategory(RecipeDataHelper.GetRecipeCategoryPayload());
        await _ingredientService.NewIngredientCategory(IngredientDataHelper.GetIngredientCategoryPayload());
        await _chefService.CreateAccount(UserDataHelper.GetChefPayload());
        await _customerService.CreateAccount(UserDataHelper.GetCustomerPayload());
        foreach(var ingredient in RecipeDataHelper.GetIngredients()) {
            await _ingredientService.NewIngredient(ingredient);
        }
        foreach(var recipe in RecipeDataHelper.GetRecipes()) {
            await _recipeService.NewRecipe(recipe);
        }
    }

    [OneTimeTearDown]
    public async Task TearDown() {
        //await _context.Database.EnsureDeletedAsync();
        ApplicationHostHelper.TearDownHost();
    }

    [Test, Order(1)]
    public async Task NewOrder() {
        Order order = new Order {
            CustomerId = 1,
        };
        order = await _orderService.NewOrder(order);
        Assert.That(order, Is.Not.Null);
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
        Order order = await _orderService.GetOrderById(1);
        Assert.That(order.FoodItems!, Has.Count.EqualTo(2));
    }
}
