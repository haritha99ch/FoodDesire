namespace FoodDesire.Web.API.Contracts.Services;
public interface IRecipeControllerService {
    Task<Recipe> GetRecipeByIdAsync(int id);
    Task<IEnumerable<Recipe>> GetRecipesAsync();
    Task<IEnumerable<Recipe>> GetRecipesAsync(string search);
    Task<FoodItem> CreateFoodItemAsync(FoodItem foodItem);
    Task<List<RecipeReview>> GetReviewsForRecipe(int recipeId);
    Task<RecipeReview> AddReviewForRecipe(RecipeReview recipeReview);
}
