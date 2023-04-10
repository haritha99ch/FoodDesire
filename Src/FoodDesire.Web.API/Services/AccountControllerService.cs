namespace FoodDesire.Web.API.Services;
public class AccountControllerService : IAccountControllerService {
    private readonly IUserService<Customer> _userService;

    public AccountControllerService(IUserService<Customer> userService) {
        _userService = userService;
    }

    public async Task<Customer> CreateAccount(User user) {
        Customer customer = new() {
            User = user,
        };
        return await _userService.CreateAccount(customer);
    }

    public async Task<Customer> GetById(int customerId) => await _userService.GetById(customerId);

    public async Task<Customer> SignIn(string email, string password) => await _userService.GetByEmailAndPassword(email, password);
}
