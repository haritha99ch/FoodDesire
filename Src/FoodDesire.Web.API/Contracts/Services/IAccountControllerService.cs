namespace FoodDesire.Web.API.Contracts.Services;
public interface IAccountControllerService {
    Task<Customer> CreateAccount(User user);
    Task<string> SignIn(string email, string password);
    Task<Customer> GetByEmail(string customerEmail);
    Task<Customer> UpdateAccount(Customer customer);
    Task<bool> DeleteById(int id);
}
