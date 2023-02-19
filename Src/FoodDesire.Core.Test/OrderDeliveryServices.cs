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

    public OrderDeliveryServices() {
        ApplicationHostHelper.ConfigureHost("OrderDeliveryServices");

        _orderDeliveryServices = ApplicationHostHelper.GetService<IOrderDeliveryService>();
        _orderService = ApplicationHostHelper.GetService<IOrderService>();
        _customerService = ApplicationHostHelper.GetService<ICustomerService>();
        _chefService = ApplicationHostHelper.GetService<IChefService>();
        _recipeService = ApplicationHostHelper.GetService<IRecipeService>();
        _ingredientService = ApplicationHostHelper.GetService<IIngredientService>();
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
        foreach (var ingredient in RecipeDataHelper.GetIngredients()) {
            await _ingredientService.NewIngredient(ingredient);
        }
        foreach (var recipe in RecipeDataHelper.GetRecipes()) {
            await _recipeService.NewRecipe(recipe);
        }
        foreach (var foodItem in RecipeDataHelper.GetRecipes()) { }
    }

    [OneTimeTearDown]
    public async Task TearDown() {
        await _context.Database.EnsureDeletedAsync();
        ApplicationHostHelper.TearDownHost();
    }
}
