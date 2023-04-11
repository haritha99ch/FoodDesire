namespace FoodDesire.Web.API.Controllers;
[ApiController]
[Route("api/[controller]")]
public class HomeController : ControllerBase {
    private readonly IHomeControllerService _homeControllerService;

    public HomeController(IHomeControllerService homeControllerService) {
        _homeControllerService = homeControllerService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Recipe>>> Index(int? customerId = null) =>
        Ok((customerId == null) ? await _homeControllerService.GetTop10Recipes()
            : await GetPredictedRecipes((int)customerId));

    private async Task<IEnumerable<Recipe>> GetPredictedRecipes(int customerId) => await _homeControllerService.GetPredictedRecipes(customerId);
}

