using FoodDesire.DAL.Contracts.Repositories;

namespace FoodDesire.Core;
public class SupplierService: ISupplierService {
    private readonly IRepository<Supplier> _supplierRepository;
    private readonly ITrackingRepository<User> _userRepository;
    public SupplierService(
        IRepository<Supplier> supplierRepository,
        ITrackingRepository<User> userRepository
        ) {
        _supplierRepository = supplierRepository;
        _userRepository = userRepository;
    }

    public async Task<Supplier> CreateAccount(Supplier user) {
        Supplier newSupplier = await _supplierRepository.Add(user);
        return newSupplier;
    }

    public async Task<bool> DeleteAccountById(int id) {
        Supplier supplire = await GetByIdPopulated(id);
        bool supplierDeleted = await _userRepository.SoftDelete(supplire!.Employee!.UserId);
        await _userRepository.SaveChanges();
        return supplierDeleted;
    }

    public async Task<List<Supplier>> GetAll() {
        List<Supplier> suppliers = await _supplierRepository.GetAll();
        return suppliers;
    }

    public async Task<Supplier> GetByEmailAndPassword(string email, string password) {
        Expression<Func<Supplier, bool>> filter =
            e => e.Employee!.User!.Account!.Email.Equals(email) &&
            e.Employee!.User!.Account.Password.Equals(password);
        Supplier? supplier = await _supplierRepository.GetOne(filter);
        return supplier!;
    }

    public async Task<Supplier> GetByIdPopulated(int id) {
        Supplier supplier = await _supplierRepository.GetByID(id);
        return supplier;
    }

    public async Task<Supplier> UpdateAccount(Supplier user) {
        Supplier updatedSupplier = await _supplierRepository.Update(user);
        await _userRepository.SaveChanges();
        return updatedSupplier;
    }
}
