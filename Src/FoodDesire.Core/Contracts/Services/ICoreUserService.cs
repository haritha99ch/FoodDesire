namespace FoodDesire.Core.Contracts.Services;
public interface ICoreUserService {
    Task<User> GetUserByAccountId(int accountId);
    Task<User> UpdateCoreUser(User user);
}
