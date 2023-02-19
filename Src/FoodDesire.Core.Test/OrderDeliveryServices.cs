namespace FoodDesire.Core.Test;

[TestFixture]
public class OrderDeliveryServices {
    private readonly IOrderDeliveryService _orderDeliveryServices;
    private readonly IOrderService _orderService;
    private readonly FoodDesireContext _context;
    private readonly ICustomerService _customerService;
    private readonly IChefService _chefService;
    private readonly IRecipeService _recipeService;
    private readonly IIngredientService _ingredientService;
    private readonly IFoodItemService _foodItemService;
    private readonly IDelivererService _delivererService;

    public OrderDeliveryServices() {
        ApplicationHostHelper.ConfigureHost("OrderDeliveryServices");

        _orderDeliveryServices = ApplicationHostHelper.GetService<IOrderDeliveryService>();
        _orderService = ApplicationHostHelper.GetService<IOrderService>();
        _customerService = ApplicationHostHelper.GetService<ICustomerService>();
        _chefService = ApplicationHostHelper.GetService<IChefService>();
        _recipeService = ApplicationHostHelper.GetService<IRecipeService>();
        _ingredientService = ApplicationHostHelper.GetService<IIngredientService>();
        _foodItemService = ApplicationHostHelper.GetService<IFoodItemService>();
        _delivererService = ApplicationHostHelper.GetService<IDelivererService>();
        _context = ApplicationHostHelper.GetService<FoodDesireContext>();
    }

    [OneTimeSetUp]
    public async Task Setup() {
        await _context.Database.EnsureDeletedAsync();
        await _context.Database.EnsureCreatedAsync();

        await _recipeService.NewRecipeCategory(RecipeDataHelper.GetRecipeCategoryPayload());
        await _ingredientService.NewIngredientCategory(
            IngredientDataHelper.GetIngredientCategoryPayload()
        );
        await _chefService.CreateAccount(UserDataHelper.GetChefPayload());
        await _customerService.CreateAccount(UserDataHelper.GetCustomerPayload());
        await _delivererService.CreateAccount(UserDataHelper.GetDelivererPayload());
        foreach (var ingredient in RecipeDataHelper.GetIngredients()) {
            await _ingredientService.NewIngredient(ingredient);
        }
        foreach (var recipe in RecipeDataHelper.GetRecipes()) {
            await _recipeService.NewRecipe(recipe);
        }
        // Create ann order and add a fooditem

        FoodItem fooditem = new() {
            RecipeId = 1,
            Order = new() {
                CustomerId = 1
            }
        };
        await _foodItemService.NewFoodItem(fooditem);
        await _foodItemService.NewFoodItem(new FoodItem() {
            RecipeId = 2,
            OrderId = 1
        });
    }

    [OneTimeTearDown]
    public async Task TearDown() {
        // await _context.Database.EnsureDeletedAsync();
        ApplicationHostHelper.TearDownHost();
    }

    [Test, Order(1)]
    public async Task NewDelivery() {
        Delivery delivery = new Delivery {
            OrderId = 1,
            Address = new Address {
                City = "Lagos",
                Street1 = "Ogunlana Drive",
                Street2 = "Surulere",
                No = "No 1",
            },
            DelivererId = 1,
        };
        Delivery newDelivery = await _orderDeliveryServices.NewDeliveryForOrder(delivery);

        Assert.That(newDelivery.IsDelivered, Is.False);
    }

}
