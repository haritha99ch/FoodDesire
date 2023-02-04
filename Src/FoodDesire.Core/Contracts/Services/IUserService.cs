namespace FoodDesire.Core.Contracts.Services;
public interface IUserService<T> {
    Task<T> CreateAccount(T user);
    Task<T> GetByIdPopulated(int id);
    Task<List<T>> GetAll();
    Task<T> GetByEmailAndPassword(string email, string password);
    Task<T> UpdateAccount(T user);
    Task<bool> DeleteAccountById(int id);
}
