namespace FoodDesire.Core.Contracts.Services;
public interface IAuthenticationService {
    Task<Account> AuthenticateUser(string clientId);
}
