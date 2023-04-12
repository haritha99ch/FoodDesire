using FoodDesire.Web.Client.Contracts.Services;
using System.Net.Http.Json;

namespace FoodDesire.Web.Client.Services;
public class RecipePageService : IRecipePageService {
    private readonly HttpClient _httpClient;

    public RecipePageService(HttpClient httpClient) {
        _httpClient = httpClient;
    }

    public async Task<List<Recipe>?> GetRecipesBySearchAsync(string? search) => await _httpClient.GetFromJsonAsync<List<Recipe>?>($"api/Recipe?search={search}");

    public async Task<FoodItem?> AddFoodItemToCart(FoodItem foodItem) {
        HttpResponseMessage response = await _httpClient.PostAsJsonAsync("api/Recipe/AddToCart", foodItem);
        return await response.Content.ReadFromJsonAsync<FoodItem>();
    }

}
