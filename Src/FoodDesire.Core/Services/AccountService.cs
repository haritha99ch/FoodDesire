namespace FoodDesire.Core.Services;
public class AccountService : IAccountService {
    private readonly IRepository<Account> _accountRepository;

    public AccountService(IRepository<Account> accountRepository) {
        _accountRepository = accountRepository;
    }

    public async Task<Account> GetAccountByEmail(string email) {
        Expression<Func<Account, bool>> filterExpression = e => e.Email == email;

        IQueryable<Account> filter(IQueryable<Account> e) => e.Where(filterExpression);

        Account account = await _accountRepository.GetOne(filter);
        return account;
    }
}
