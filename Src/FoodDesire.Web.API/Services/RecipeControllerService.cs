namespace FoodDesire.Web.API.Services;
public class RecipeControllerService : IRecipeControllerService {
    private readonly IRecipeService _recipeService;

    public RecipeControllerService(IRecipeService recipeService) {
        _recipeService = recipeService;
    }

    public async Task<Recipe> GetRecipeByIdAsync(int id) => await _recipeService.GetRecipeById(id);

    public async Task<IEnumerable<Recipe>> GetRecipesAsync() => await _recipeService.GetAllRecipesWithCategory(true);

    public async Task<IEnumerable<Recipe>> GetRecipesAsync(string search) => await _recipeService.SearchRecipes(search, true);
}
