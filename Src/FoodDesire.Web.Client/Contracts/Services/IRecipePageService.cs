namespace FoodDesire.Web.Client.Contracts.Services;
public interface IRecipePageService {
    Task<List<RecipeListItem>> GetRecipesBySearchAsync(string? search);
    Task<RecipeDetail> GetRecipeByIdAsync(int id);
    Task<FoodItem?> AddFoodItemToCartAsync(FoodItem foodItem);
    Task<Order> GetCurrentUserExistingOrderAsync();
    Task<List<RecipeReview>> GetRecipeReviewsForRecipeAsync(int recipeId);
    Task<RecipeReview> AddRecipeReviewsForRecipeAsync(RecipeReview recipeReview);
}
