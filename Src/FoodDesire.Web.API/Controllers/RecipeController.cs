namespace FoodDesire.Web.API.Controllers;
[ApiController]
[Route("api/[controller]")]
public class RecipeController : ControllerBase {
    private readonly IRecipeControllerService _recipeControllerService;

    public RecipeController(IRecipeControllerService recipeControllerService) {
        _recipeControllerService = recipeControllerService;
    }

    [HttpGet]
    public async Task<IEnumerable<Recipe>> Index(string? search) {
        if (string.IsNullOrEmpty(search) || string.IsNullOrWhiteSpace(search))
            return await _recipeControllerService.GetRecipesAsync();

        return await _recipeControllerService.GetRecipesAsync(search);
    }

    [HttpGet("{id}")]
    public async Task<Recipe> Details(int id) {
        return await _recipeControllerService.GetRecipeByIdAsync(id);
    }

    [HttpPost]
    public async Task<FoodItem> AddToCart(FoodItem foodItem) {
        return await _recipeControllerService.CreateFoodItemAsync(foodItem);
    }
}
