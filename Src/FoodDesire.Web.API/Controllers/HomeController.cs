namespace FoodDesire.Web.API.Controllers;
[ApiController]
[Route("api/[controller]")]
public class HomeController : ControllerBase {
    private readonly IHomeControllerService _homeControllerService;

    public HomeController(IHomeControllerService homeControllerService) {
        _homeControllerService = homeControllerService;
    }

    [HttpGet]
    public async Task<IEnumerable<Recipe>> Index(int? CustomerId = null) {
        if (CustomerId == null) return await _homeControllerService.GetTop10Recipes();
        // TODO: Recommendation Process 
        return await _homeControllerService.GetTop10Recipes();
    }
}
