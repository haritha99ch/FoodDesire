namespace FoodDesire.Web.API.Services;
public class HomeControllerService : IHomeControllerService {
    private readonly IRecipeService _recipeService;
    private readonly IPredictionService _predictionService;

    public HomeControllerService(IRecipeService recipeService, IPredictionService predictionService) {
        _recipeService = recipeService;
        _predictionService = predictionService;
    }

    public async Task<IEnumerable<Recipe>> GetTop10Recipes() => await _recipeService.GetTop10Recipes();

    public async Task<IEnumerable<Recipe>> GetPredictedRecipes(int customerId) {
        List<Recipe>? allRecipes = await _recipeService.GetAllRecipesWithCategory();
        if (!_predictionService.EnsureModelLoaded()) return allRecipes.Take(10);

        List<(Recipe Recipe, float Score)>? predictions = new List<(Recipe Recipe, float Score)>();
        foreach (Recipe recipe in allRecipes) {
            RecipePrediction? prediction = _predictionService.RecipePrediction(customerId, recipe.Id);
            predictions.Add((recipe, prediction.Score));
        }

        IEnumerable<Recipe>? recommendedRecipes = predictions.OrderByDescending(e => e.Score)
            .Take(10).Select(e => e.Recipe);

        return recommendedRecipes;
    }

}
