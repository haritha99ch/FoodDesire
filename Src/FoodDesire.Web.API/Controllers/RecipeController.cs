namespace FoodDesire.Web.API.Controllers;
[ApiController]
[Route("api/[controller]")]
public class RecipeController : ControllerBase {
    private readonly IRecipeControllerService _recipeControllerService;
    private readonly ICartControllerService _cartControllerService;

    public RecipeController(IRecipeControllerService recipeControllerService, ICartControllerService cartControllerService) {
        _recipeControllerService = recipeControllerService;
        _cartControllerService = cartControllerService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Recipe>>> Index(string? search) =>
         string.IsNullOrEmpty(search) || string.IsNullOrWhiteSpace(search)
            ? Ok(await _recipeControllerService.GetRecipesAsync())
            : Ok(await _recipeControllerService.GetRecipesAsync(search));

    [HttpGet(nameof(recipeId))]
    public async Task<ActionResult<Recipe>> Details(int recipeId) => Ok(await _recipeControllerService.GetRecipeByIdAsync(recipeId));

    [HttpPost(nameof(AddToCart)), Authorize]
    public async Task<ActionResult<FoodItem>> AddToCart(FoodItem foodItem) {
        string? userId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
        if (foodItem.Order == null) {
            Order? order = await _cartControllerService.GetOrderAsync((int)foodItem.OrderId!);
            if (order.CustomerId != int.Parse(userId!)) return BadRequest("You are not authorized to perform this action!");
        }
        return Ok(await _recipeControllerService.CreateFoodItemAsync(foodItem));
    }
}
