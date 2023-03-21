namespace FoodDesire.Core.Services;
public class CoreUserService : ICoreUserService {
    private readonly IRepository<User> _coreUserRepository;

    public CoreUserService(IRepository<User> coreUserRepository) {
        _coreUserRepository = coreUserRepository;
    }

    public async Task<List<User>> GetAllEmployees() {
        Expression<Func<User, bool>> filter = e => e.Account!.Role != Role.Admin && e.Account.Role != Role.Customer;
        Func<IQueryable<User>, IIncludableQueryable<User, object>> include = e => e.Include(e => e.Account)!;
        List<User> users = await _coreUserRepository.Get(filter, null, include);
        return users;
    }

    public async Task<User> GetUserByAccountId(int accountId) {
        Expression<Func<User, bool>> filter = e => e!.AccountId == accountId;
        Func<IQueryable<User>, IIncludableQueryable<User, object>> include = e => e.Include(e => e.Account!);
        User user = await _coreUserRepository.GetOne(filter, include);
        return user;
    }

    public async Task<User> UpdateCoreUser(User user) {
        User updatedUser = await _coreUserRepository.Update(user);
        return updatedUser;
    }
}
