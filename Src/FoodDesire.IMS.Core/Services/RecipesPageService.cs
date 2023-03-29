namespace FoodDesire.IMS.Core.Services;
public class RecipesPageService : IRecipesPageService {
    private readonly IRecipeService _recipeService;

    public RecipesPageService(IRecipeService recipeService) {
        _recipeService = recipeService;
    }


    public async Task<Recipe> AddNewRecipe(Recipe recipe) => await _recipeService.NewRecipe(recipe);

    public async Task<bool> DeleteRecipeById(int recipeId) => await _recipeService.RemoveRecipeById(recipeId);

    public async Task<Recipe> EditRecipe(Recipe recipe) => await _recipeService.UpdateRecipe(recipe);

    public async Task<List<Recipe>> GetAllRecipeByCategoryId(int categoryId) => await _recipeService.GetAllRecipesByCategoryId(categoryId);

    public async Task<List<Recipe>> GetAllRecipes() => await _recipeService.GetAllRecipes();

    public async Task<Recipe> GetRecipeById(int recipeId) => await _recipeService.GetRecipeById(recipeId);
}
