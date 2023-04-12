namespace FoodDesire.Web.Client.Contracts.Services;
public interface IRecipePageService {
    Task<List<Recipe>?> GetRecipesBySearchAsync(string? search);
    Task<FoodItem?> AddFoodItemToCart(FoodItem foodItem);
}
