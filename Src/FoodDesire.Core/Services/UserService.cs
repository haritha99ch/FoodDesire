namespace FoodDesire.Core.Services;
public class UserService<T> : IUserService<T> where T : BaseUser {
    private readonly IRepository<T> _userRepository;
    private readonly IRepository<User> _coreUserRepository;
    private readonly IRepository<Account> _accountRepository;

    public UserService(IRepository<T> userRepository, IRepository<User> coreUserRepository, IRepository<Account> accountRepository) {
        _userRepository = userRepository;
        _coreUserRepository = coreUserRepository;
        _accountRepository = accountRepository;
    }
    public async Task<T> CreateAccount(T user) {
        T newUser = await _userRepository.Add(user);
        return newUser;
    }

    public async Task<bool> DeleteAccountById(int id) {
        T user = await _userRepository.GetByID(id);
        User coreUser = await _coreUserRepository.GetByID(user.UserId);
        bool userDeleted = await _userRepository.Delete(id);
        bool accountDeleted = await _accountRepository.Delete(coreUser.AccountId);
        bool coreUserDeleted = await _coreUserRepository.Delete(coreUser.Id);
        return userDeleted;
    }

    public async Task<List<T>> GetAll() {
        List<T> users = await _userRepository.GetAll();
        return users;
    }

    public async Task<T> GetByEmailAndPassword(string email, string password) {
        Expression<Func<T, bool>> filterExpression =
            e => e.User!.Account!.Email!.Equals(email) && e.User!.Account!.Password!.Equals(password);

        IQueryable<T> filter(IQueryable<T> e) => e.Where(filterExpression);
        IIncludableQueryable<T, object> include(IQueryable<T> e) => e.Include(e => e.User!).ThenInclude(e => e.Account!);

        T user = await _userRepository.GetOne(filter, include);
        return user;
    }

    public async Task<T> GetById(int id) {
        Expression<Func<T, bool>> filterExpression = e => e.Id == id;

        IQueryable<T> filter(IQueryable<T> e) => e.Where(filterExpression);
        IIncludableQueryable<T, object> include(IQueryable<T> e) => e.Include(e => e.User!).ThenInclude(e => e.Account!);

        T user = await _userRepository.GetOne(filter, include);

        return user;
    }

    public async Task<T> UpdateAccount(T user) {
        T updatedUser = await _userRepository.Update(user);
        return updatedUser;
    }

    public async Task<T> GetByEmail(string email) {
        Expression<Func<T, bool>> filterExpression = e => e!.User!.Account!.Email!.Equals(email);

        IQueryable<T> filter(IQueryable<T> e) => e.Where(filterExpression);
        IIncludableQueryable<T, object> include(IQueryable<T> e) => e.Include(e => e.User!).ThenInclude(e => e.Account!);

        T? user = await _userRepository.GetOne(filter, include);
        return user;
    }
}
