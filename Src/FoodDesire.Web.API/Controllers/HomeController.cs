namespace FoodDesire.Web.API.Controllers;
[ApiController]
[Route("api/[controller]")]
public class HomeController : ControllerBase {
    private readonly IHomeControllerService _homeControllerService;

    public HomeController(IHomeControllerService homeControllerService) {
        _homeControllerService = homeControllerService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Recipe>>> Index() {
        string? userId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
        return Ok(string.IsNullOrEmpty(userId) ? await _homeControllerService.GetTop10Recipes()
            : await GetPredictedRecipes(int.Parse(userId)));
    }

    private async Task<IEnumerable<Recipe>> GetPredictedRecipes(int customerId) => await _homeControllerService.GetPredictedRecipes(customerId);
}

