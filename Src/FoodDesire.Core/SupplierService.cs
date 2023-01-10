namespace FoodDesire.Core;
public class SupplierService: ISupplierService {
    private readonly IRepository<Supplier> _supplierRepository;
    private readonly TrackingRepository<User> _userRepository;
    private readonly FoodDesireContext _context;
    public SupplierService(
        FoodDesireContext context,
        IRepository<Supplier> supplierRepository,
        TrackingRepository<User> userRepository
        ) {
        _context = context;
        _supplierRepository = supplierRepository;
        _userRepository = userRepository;
    }

    public async Task<Supplier> CreateAccount(Supplier user) {
        Supplier newSupplier = await _supplierRepository.Add(user);
        await _context.SaveChangesAsync();
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
        Supplier? supplier = await _context.Set<Supplier>()
            .AsNoTracking().Include(e => e.Employee)
            .ThenInclude(e => e!.User)
            .ThenInclude(u => u!.Account)
            .SingleAsync(filter);
        return supplier!;
    }

    public async Task<Supplier> GetByIdPopulated(int id) {
        Supplier supplier = await _context.Set<Supplier>()
            .AsNoTracking()
            .Include(e => e.Employee)
            .ThenInclude(e => e!.User)
            .ThenInclude(u => u!.Account)
            .Include(e => e.Employee!.User!.Address)
            .SingleAsync(e => e.Id == id);
        return supplier;
    }

    public async Task<Supplier> UpdateAccount(Supplier user) {
        Supplier updatedSupplier = await _supplierRepository.Update(user);
        await _userRepository.SaveChanges();
        return updatedSupplier;
    }
}
