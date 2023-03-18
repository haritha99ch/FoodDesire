namespace FoodDesire.Core.Contracts.Services;
public interface IAccountService {
    Task<Account> GetAccountByEmail(string email);
}
