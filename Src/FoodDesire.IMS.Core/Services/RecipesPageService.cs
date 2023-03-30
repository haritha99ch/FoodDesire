namespace FoodDesire.IMS.Core.Services;
public class RecipesPageService : IRecipesPageService {
    private readonly IRecipeService _recipeService;

    public RecipesPageService(IRecipeService recipeService) {
        _recipeService = recipeService;
    }


    public async Task<Recipe> AddNewRecipe(Recipe recipe) => await _recipeService.NewRecipe(recipe);

    public async Task<RecipeCategory> AddNewRecipeCategory(RecipeCategory recipeCategory) => await _recipeService.NewRecipeCategory(recipeCategory);

    public async Task<bool> DeleteRecipeById(int recipeId) => await _recipeService.RemoveRecipeById(recipeId);

    public async Task<Recipe> EditRecipe(Recipe recipe) => await _recipeService.UpdateRecipe(recipe);

    public async Task<RecipeCategory> EditRecipeCategory(RecipeCategory recipeCategory) => await _recipeService.UpdateRecipeCategory(recipeCategory);

    public async Task<List<Recipe>> GetAllRecipeByCategoryId(int categoryId) => await _recipeService.GetAllRecipesByCategoryId(categoryId);

    public async Task<List<RecipeCategory>> GetAllRecipeCategories() => await _recipeService.GetAllRecipeCategories();

    public async Task<List<Recipe>> GetAllRecipes() => await _recipeService.GetAllRecipes();

    public async Task<Recipe> GetRecipeById(int recipeId) => await _recipeService.GetRecipeById(recipeId);

    public async Task<RecipeCategory> GetRecipeCategoryById(int recipeCategoryId) => await _recipeService.GetRecipeCategoryById(recipeCategoryId);

    public async Task<RecipeCategory> GetRecipeCategoryByName(string recipeCategoryName) => await _recipeService.GetRecipeCategoryByName(recipeCategoryName);
}
