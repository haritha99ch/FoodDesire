namespace FoodDesire.Core.Services;
public class CoreUserService : ICoreUserService {
    private readonly IRepository<User> _coreUserRepository;

    public CoreUserService(IRepository<User> coreUserRepository) {
        _coreUserRepository = coreUserRepository;
    }

    public async Task<List<User>> GetAllEmployees() {
        Expression<Func<User, bool>> filterExpression = e => e.Account!.Role != Role.Admin && e.Account.Role != Role.Customer;

        IIncludableQueryable<User, object> include(IQueryable<User> e) => e.Include(e => e.Account!);
        IQueryable<User> filter(IQueryable<User> e) => e.Where(filterExpression);

        List<User> users = await _coreUserRepository.Get(filter, null, include);
        return users;
    }

    public async Task<User> GetUserByAccountId(int accountId) {
        Expression<Func<User, bool>> filterExpression = e => e!.AccountId == accountId;

        IIncludableQueryable<User, object> include(IQueryable<User> e) => e.Include(e => e.Account!);
        IQueryable<User> filter(IQueryable<User> e) => e.Where(filterExpression);

        User user = await _coreUserRepository.GetOne(filter, include);
        return user;
    }

    public async Task<User> UpdateCoreUser(User user) {
        User updatedUser = await _coreUserRepository.Update(user);
        return updatedUser;
    }
}
