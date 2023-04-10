namespace FoodDesire.Web.API.Contracts.Services;
public interface IAccountControllerService {
    Task<Customer> CreateAccount(User user);
    Task<Customer> SignIn(string email, string password);
    Task<Customer> GetById(int customerId);
}
