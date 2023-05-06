namespace FoodDesire.Web.API.Services;
public class HomeControllerService : IHomeControllerService {
    private readonly IRecipeService _recipeService;
    private readonly IPredictionService _predictionService;
    private readonly IRecipeReviewService _recipeReviewService;

    public HomeControllerService(IRecipeService recipeService, IPredictionService predictionService, IRecipeReviewService recipeReviewService) {
        _recipeService = recipeService;
        _predictionService = predictionService;
        _recipeReviewService = recipeReviewService;
    }

    public async Task<IEnumerable<Recipe>> GetTop10Recipes() => await _recipeService.GetTop10Recipes();

    public async Task<IEnumerable<Recipe>> GetPredictedRecipes(int customerId) {
        List<Recipe>? allRecipes = await _recipeService.GetAllRecipesWithCategory(true);
        if (!await _predictionService.EnsureModelLoaded()) return allRecipes.Take(10);

        List<(Recipe Recipe, float Score)>? predictions = new();
        foreach (Recipe recipe in allRecipes) {
            RecipePrediction? prediction = _predictionService.RecipePrediction(customerId, recipe.Id);
            predictions.Add((recipe, prediction.Score));
        }

        IEnumerable<Recipe>? recommendedRecipes = predictions.OrderByDescending(e => e.Score)
            .Take(10).Select(e => e.Recipe);

        List<RecipeReview> reviews = await _recipeReviewService.GetReviewsReviewedByCustomer(customerId);

        if (!reviews.Any()) return recommendedRecipes;

        var newRecommendedRecipes = recommendedRecipes.Where(e => !reviews.Any(r => r.RecipeId == e.Id)).Take(10);

        if (newRecommendedRecipes.Count() < 10) {
            int remaining = 10 - newRecommendedRecipes.Count();
            newRecommendedRecipes = newRecommendedRecipes.Concat(recommendedRecipes.Take(remaining));
        }
        return recommendedRecipes;
    }

}
