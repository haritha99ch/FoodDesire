namespace FoodDesire.Core.Services;
public class UserService<T> : IUserService<T> where T : BaseUser {
    private readonly IRepository<T> _userRepository;
    public UserService(IRepository<T> userRepository) {
        _userRepository = userRepository;
    }
    public async Task<T> CreateAccount(T user) {
        T newUser = await _userRepository.Add(user);
        return newUser;
    }

    public async Task<bool> DeleteAccountById(int id) {
        bool userDeleted = await _userRepository.Delete(id);
        return userDeleted;
    }

    public async Task<List<T>> GetAll() {
        List<T> users = await _userRepository.GetAll();
        return users;
    }

    public async Task<T> GetByEmailAndPassword(string email, string password) {
        Expression<Func<T, bool>> filter =
            e => e.User!.Account!.Email.Equals(email) && e.User!.Account!.Password!.Equals(password);

        T user = await _userRepository.GetOne(filter);
        return user;
    }

    public async Task<T> GetById(int id) {
        T user = await _userRepository.GetByID(id);
        return user;
    }

    public async Task<T> UpdateAccount(T user) {
        T updatedUser = await _userRepository.Update(user);
        return updatedUser;
    }

    public async Task<T> GetByEmail(string email) {
        Expression<Func<T, bool>> filter = e => e!.User!.Account!.Email.Equals(email);

        T? user = await _userRepository.GetOne(filter);
        return user;
    }
}
