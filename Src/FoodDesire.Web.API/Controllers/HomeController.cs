namespace FoodDesire.Web.API.Controllers;
[ApiController]
[Route("api/[controller]")]
public class HomeController : ControllerBase {
    private readonly IHomeControllerService _homeControllerService;

    public HomeController(IHomeControllerService homeControllerService) {
        _homeControllerService = homeControllerService;
    }

    [HttpGet]
    public async Task<IEnumerable<Recipe>> Index(int? customerId = null) {
        return (customerId == null) ? await _homeControllerService.GetTop10Recipes()
            : await GetPredictedRecipes((int)customerId);
    }

    private async Task<IEnumerable<Recipe>> GetPredictedRecipes(int customerId) {
        return await _homeControllerService.GetPredictedRecipes(customerId);
    }
}
