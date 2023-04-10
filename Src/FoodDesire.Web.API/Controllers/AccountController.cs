namespace FoodDesire.Web.API.Controllers;
[ApiController]
[Route("api/[controller]")]
public class AccountController : ControllerBase {
    private readonly IAccountControllerService _accountControllerService;

    public AccountController(IAccountControllerService accountControllerService) {
        _accountControllerService = accountControllerService;
    }

    [HttpGet(nameof(Index))]
    public Task<Customer> Index() {
        //TODO: Get the customer id from the authentication
        return _accountControllerService.GetById(1);
    }

    [HttpPost(nameof(SignUp))]
    public Task<Customer> SignUp(User user) => _accountControllerService.CreateAccount(user);

    [HttpPost(nameof(SignIn))]
    public Task<Customer> SignIn(string email, string password) => _accountControllerService.SignIn(email, password);
}
