namespace FoodDesire.Core.Test;
[TestFixture]
public class FoodOrderServices {
    private readonly IFoodItemService _foodItemService;
    private readonly IIngredientService _ingredientService;
    private readonly IRecipeService _recipeService;
    private readonly IChefService _chefService;
    private readonly FoodDesireContext _context;
    public FoodOrderServices() {
        ApplicationHostHelper.ConfigureHost("FoodOrderServices");

        _foodItemService = ApplicationHostHelper.GetService<IFoodItemService>();
        _ingredientService = ApplicationHostHelper.GetService<IIngredientService>();
        _recipeService = ApplicationHostHelper.GetService<IRecipeService>();
        _chefService = ApplicationHostHelper.GetService<IChefService>();
        _context = ApplicationHostHelper.GetService<FoodDesireContext>();
    }

    [OneTimeSetUp]
    public async Task Setup() {
        await _context.Database.EnsureDeletedAsync();
        await _context.Database.EnsureCreatedAsync();

        await _recipeService.NewRecipeCategory(RecipeDataHelper.GetRecipeCategoryPayload());
        await _ingredientService.NewIngredientCategory(IngredientDataHelper.GetIngredientCategoryPayload());
        await _chefService.CreateAccount(UserDataHelper.GetChefPayload());
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

    [Test]
    public async Task NewFoodItem() {
        FoodItem foodItem = new FoodItem {
            RecipeId = 2,
            Order = new Order() {
                Customer = UserDataHelper.GetCustomerPayload(),
            }
        };
        foodItem = await _foodItemService.NewFoodItem(foodItem);
        Assert.That(foodItem.Price, Is.EqualTo(foodItem.Recipe!.FixedPrice));
    }
}
