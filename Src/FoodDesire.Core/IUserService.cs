namespace FoodDesire.Core;
public interface IUserService<T> where T : User {
    Task<T> CreateAccount(T user);
    Task<T> GetById(int id);
    Task<List<T>> GetAll();
    Task<T> GetByEmailAndPassword(string email, string password);
    Task<T> UpdateAccount(T user);
    Task<bool> DeleteAccountById(int id);
}
