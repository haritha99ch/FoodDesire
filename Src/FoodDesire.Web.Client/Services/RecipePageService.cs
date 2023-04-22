namespace FoodDesire.Web.Client.Services;
public class RecipePageService : IRecipePageService {
    private readonly HttpClient _httpClient;

    public RecipePageService(HttpClient httpClient) {
        _httpClient = httpClient;
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

    public async Task<FoodItem?> AddFoodItemToCart(FoodItem foodItem) {
        HttpResponseMessage response = await _httpClient.PostAsJsonAsync("api/Recipe/AddToCart", foodItem);
        return await response.Content.ReadFromJsonAsync<FoodItem>();
    }

}
