namespace FoodDesire.Web.API.Services;
public class RecipeControllerService : IRecipeControllerService {
    private readonly IRecipeService _recipeService;
    private readonly IFoodItemService _foodItemService;

    public RecipeControllerService(IRecipeService recipeService, IFoodItemService foodItemService) {
        _recipeService = recipeService;
        _foodItemService = foodItemService;
    }

    public async Task<Recipe> GetRecipeByIdAsync(int id) => await _recipeService.GetRecipeById(id);

    public async Task<IEnumerable<Recipe>> GetRecipesAsync() => await _recipeService.GetAllRecipesWithCategory(true);

    public async Task<IEnumerable<Recipe>> GetRecipesAsync(string search) => await _recipeService.SearchRecipes(search, true);

    public async Task<FoodItem> CreateFoodItemAsync(FoodItem foodItem) => await _foodItemService.NewFoodItem(foodItem);
}
