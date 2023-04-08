namespace FoodDesire.Web.API.Services;
public class HomeControllerService : IHomeControllerService {
    private readonly IRecipeService _recipeService;

    public HomeControllerService(IRecipeService recipeService) {
        _recipeService = recipeService;
    }

    public async Task<IEnumerable<Recipe>> GetTop10Recipes() => await _recipeService.GetTop10Recipes();
}
