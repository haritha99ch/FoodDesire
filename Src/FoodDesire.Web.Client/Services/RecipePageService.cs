namespace FoodDesire.Web.Client.Services;
public class RecipePageService : AuthorizedService, IRecipePageService {

    public RecipePageService(HttpClient httpClient, IAuthenticationService authenticationService) : base(httpClient, authenticationService) {
    }

    public async Task<List<RecipeListItem>> GetRecipesBySearchAsync(string? search) {
        HttpResponseMessage? response = await _httpClient.GetAsync($"api/Recipe?search={search}");
        if (response.StatusCode == HttpStatusCode.ServiceUnavailable) return new();
        try {
            return await response.Content.ReadFromJsonAsync<List<RecipeListItem>>() ?? new();
        } catch (Exception) {
            return new();
        }
    }

    public async Task<FoodItem?> AddFoodItemToCartAsync(FoodItem foodItem) {
        HttpResponseMessage response = await _httpClient.PostAsJsonAsync("api/Recipe/AddToCart", foodItem);
        return await response.Content.ReadFromJsonAsync<FoodItem>();
    }

    public async Task<RecipeDetail> GetRecipeByIdAsync(int id) {
        HttpResponseMessage? response = await _httpClient.GetAsync($"api/Recipe/recipeId?recipeId={id}");
        if (response.StatusCode != HttpStatusCode.OK) return default!;
        try {
            return await response.Content.ReadFromJsonAsync<RecipeDetail>() ?? default!;
        } catch (Exception) {
            return default!;
        }
    }

    public async Task<Order> GetCurrentUserExistingOrderAsync() {
        HttpResponseMessage? response = await (await AddAuthorizationHeader()).GetAsync($"api/Cart/Index");
        if (response.StatusCode != HttpStatusCode.OK) return default!;
        try {
            return await response.Content.ReadFromJsonAsync<Order>() ?? default!;
        } catch (Exception) {
            return default!;
        }
    }

    public async Task<FoodItem> AddFoodItemToOrderAsync(FoodItem foodItem) {
        HttpResponseMessage? response = await (await AddAuthorizationHeader()).PostAsJsonAsync($"api/Recipe/AddToCart", foodItem);
        if (response.StatusCode != HttpStatusCode.OK) return default!;
        try {
            return await response.Content.ReadFromJsonAsync<FoodItem>() ?? default!;
        } catch (Exception) {
            return default!;
        }
    }

    public async Task<List<RecipeReview>> GetRecipeReviewsForRecipeAsync(int recipeId) {
        HttpResponseMessage? response = await _httpClient.GetAsync($"api/Recipe/Reviews?recipeId={recipeId}");
        if (response.StatusCode != HttpStatusCode.OK) return default!;
        try {
            return await response.Content.ReadFromJsonAsync<List<RecipeReview>>() ?? default!;
        } catch (Exception) {
            return default!;
        }
    }

    public async Task<RecipeReview> AddRecipeReviewsForRecipeAsync(RecipeReview recipeReview) {
        HttpResponseMessage? response = await _httpClient.PostAsJsonAsync($"api/Recipe/Review", recipeReview);
        if (response.StatusCode != HttpStatusCode.OK) return default!;
        try {
            return await response.Content.ReadFromJsonAsync<RecipeReview>() ?? default!;
        } catch (Exception) {
            return default!;
        }
    }
}
