namespace FoodDesire.Core.Test;
[TestFixture]
public class RecipeServices {
    private readonly IRecipeService _recipeService;
    private readonly IChefService _chefService;
    private readonly IIngredientService _ingredientService;
    private readonly FoodDesireContext _context;

    public RecipeServices() {
        ApplicationHostHelper.ConfigureHost("RecipeServices");

        _recipeService = ApplicationHostHelper.GetService<IRecipeService>();
        _chefService = ApplicationHostHelper.GetService<IChefService>();
        _context = ApplicationHostHelper.GetService<FoodDesireContext>();
        _ingredientService = ApplicationHostHelper.GetService<IIngredientService>();
    }

    [OneTimeSetUp]
    public async Task SetUp() {
        await _context.Database.EnsureCreatedAsync();

        await _chefService.CreateAccount(UserDataHelper.GetChefPayload());
        await _ingredientService.NewIngredientCategory(IngredientDataHelper.GetIngredientCategoryPayload());
        for(int i = 0; i < 2; i++) {
            await _ingredientService.NewIngredient(IngredientDataHelper.GetIngredientPayLoad(1));
        }
    }

    [OneTimeTearDown]
    public async Task TearDown() {
        await _context.Database.EnsureDeletedAsync();
        ApplicationHostHelper.TearDownHost();
    }

    [Test, Order(1)]
    public async Task NewRecipeCategory() {
        RecipeCategory recipeCategory = await _recipeService.NewRecipeCategory(RecipeDataHelper.GetRecipeCategoryPayload());
        Assert.That(recipeCategory.Name, Is.EqualTo(RecipeDataHelper.GetRecipeCategoryPayload().Name));
    }

    [Test, Order(2)]
    public async Task NewRecipe() {
        Recipe recipe = RecipeDataHelper.GetRecipePayload();

        Recipe newRecipe = await _recipeService.NewRecipe(recipe);
        Assert.Multiple(() => {
            Assert.That(newRecipe.Name, Is.EqualTo(recipe.Name));
            Assert.That(newRecipe.RecipeIngredients, Has.Count.EqualTo(2));
        });
    }

    [Test, Order(3)]
    public async Task AddIngredientToRecipe() {
        Recipe recipe = await _recipeService
            .AddRecipeIngredientToRecipe(1, RecipeDataHelper.GetRecipeIngredientPayload(3));

        Assert.That(recipe.RecipeIngredients, Has.Count.EqualTo(3));
    }


}
