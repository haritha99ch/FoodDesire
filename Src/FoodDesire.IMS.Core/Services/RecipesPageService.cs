namespace FoodDesire.IMS.Core.Services;
public class RecipesPageService : IRecipesPageService {
    private readonly IRecipeService _recipeService;
    private readonly IIngredientService _ingredientService;

    public RecipesPageService(IRecipeService recipeService, IIngredientService ingredientService) {
        _recipeService = recipeService;
        _ingredientService = ingredientService;
    }


    public async Task<Recipe> AddNewRecipe(Recipe recipe) => await _recipeService.NewRecipe(recipe);

    public async Task<RecipeCategory> AddNewRecipeCategory(RecipeCategory recipeCategory) => await _recipeService.NewRecipeCategory(recipeCategory);

    public async Task<bool> DeleteRecipeById(int recipeId) => await _recipeService.RemoveRecipeById(recipeId);

    public async Task<Recipe> EditRecipe(Recipe recipe) => await _recipeService.UpdateRecipe(recipe);

    public async Task<RecipeCategory> EditRecipeCategory(RecipeCategory recipeCategory) => await _recipeService.UpdateRecipeCategory(recipeCategory);

    public Task<List<Ingredient>> GetAllIngredients() => _ingredientService.GetAllIngredients();

    public async Task<List<Ingredient>> GetAllIngredientsWithCategory() => await _ingredientService.GetAllIngredientsWithCategory();

    public async Task<List<Recipe>> GetAllRecipeAsIngredients() => await _recipeService.GetAllRecipeAsIngredients();

    public async Task<List<Recipe>> GetAllRecipeByCategoryId(int categoryId) => await _recipeService.GetAllRecipesByCategoryId(categoryId);

    public async Task<List<RecipeCategory>> GetAllRecipeCategories() => await _recipeService.GetAllRecipeCategories();

    public async Task<List<Recipe>> GetAllRecipes() => await _recipeService.GetAllRecipesWithCategory();

    public async Task<Recipe> GetRecipeById(int recipeId) => await _recipeService.GetRecipeById(recipeId);

    public async Task<RecipeCategory> GetRecipeCategoryById(int recipeCategoryId) => await _recipeService.GetRecipeCategoryById(recipeCategoryId);

    public async Task<RecipeCategory> GetRecipeCategoryByName(string recipeCategoryName) => await _recipeService.GetRecipeCategoryByName(recipeCategoryName);

    public async Task<List<Recipe>> SearchRecipes(string value) => await _recipeService.SearchRecipes(value);
}
