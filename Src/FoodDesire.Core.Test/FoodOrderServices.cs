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
        RecipeDataHelper.GetIngredients()
            .ForEach(async x => {
                await _ingredientService.NewIngredient(x);
            });
        await _recipeService.NewRecipe(RecipeDataHelper.GetRecipe());
    }

    [OneTimeTearDown]
    public async Task TearDown() {
        //await _context.Database.EnsureDeletedAsync();
        ApplicationHostHelper.TearDownHost();
    }

    [Test, Order(1)]
    public async Task NewFoodItem() {

    }
}
