namespace FoodDesire.Web.Client.Contracts.Services;
public interface IAccountPageService {
    Task<Customer?> Get();
    Task<Customer?> SignUp(User user);
    Task<string?> SignIn(SignIn signIn);
    Task<bool> SignOut();
    Task<Customer?> Edit(Customer customer);
    Task<bool> Delete();
}
