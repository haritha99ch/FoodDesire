namespace FoodDesire.Core.Contracts.Services;
public interface IAuthenticationService {
    Task<User> NewUser();
    Task<Account> AcquireAccount();
    Task<User> UpdateUser(int userAccountId, string accessToken);
    string AcquireAccessToken();
    Task<Account> AcquireAccount(string accessToken);
    bool SignOutMSAL();
}
