namespace FoodDesire.Web.API.Controllers;
[ApiController]
[Route("api/[controller]"), Authorize]
public class AccountController : ControllerBase {
    private readonly IAccountControllerService _accountControllerService;

    public AccountController(IAccountControllerService accountControllerService) {
        _accountControllerService = accountControllerService;
    }

    [HttpGet(nameof(Index))]
    public async Task<ActionResult<Customer>> Index() {
        string? userEmail = User?.Identity?.Name;
        if (userEmail == null) return BadRequest("Invalid User");
        Customer customer = await _accountControllerService.GetByEmail(userEmail!);
        return customer == null ? BadRequest("Invalid User") : Ok(customer);
    }

    [HttpPost(nameof(SignUp)), AllowAnonymous]
    public async Task<ActionResult<Customer>> SignUp(User user) => /*Todo:Implement Email Verification*/ Ok(await _accountControllerService.CreateAccount(user));

    [HttpPost(nameof(SignIn)), AllowAnonymous]
    public async Task<ActionResult<string>> SignIn(SignIn signIn) {
        string jwt = await _accountControllerService.SignIn(signIn.Email, signIn.Password);
        return jwt == null ? BadRequest("Invalid SignIn") : Ok(jwt);
    }

    [HttpPatch(nameof(Edit))]
    public async Task<ActionResult<Customer>> Edit(Customer customer) {
        string? userEmail = User?.Identity?.Name;
        if (userEmail == null) return BadRequest("Invalid User");
        Customer updatedCustomer = await _accountControllerService.UpdateAccount(customer);
        return updatedCustomer == null ? BadRequest("Invalid User") : Ok(updatedCustomer);
    }

    [HttpDelete(nameof(Delete))]
    public async Task<ActionResult<bool>> Delete() {
        string? userEmail = User?.Identity?.Name;
        if (userEmail == null) return BadRequest("Invalid User");
        Customer customer = await _accountControllerService.GetByEmail(userEmail!);
        if (customer == null) return BadRequest("Invalid User");
        bool customerDeleted = await _accountControllerService.DeleteById(customer.Id);
        return customerDeleted ? Ok(customerDeleted) : BadRequest(customerDeleted);
    }
}
