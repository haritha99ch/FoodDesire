namespace FoodDesire.Core;
public class SupplierService<Supplier>: ISupplierService<Supplier>, IUserService<Supplier> where Supplier : User {
    private readonly ITrackingRepository<Supplier> _supplierRepository;
    private readonly IRepository<Supply> _supplyRepository;
    private readonly FoodDesireContext _context;
    public SupplierService(
        FoodDesireContext context,
        ITrackingRepository<Supplier> supplierRepository,
        IRepository<Supply> supplyRepository
        ) {
        _context = context;
        _supplierRepository = supplierRepository;
        _supplyRepository = supplyRepository;
    }

    public async Task<Supplier> CreateAccount(Supplier user) {
        Supplier newSupplier = await _supplierRepository.Add(user);
        await _context.SaveChangesAsync();
        return newSupplier;
    }

    public async Task<bool> DeleteAccountById(int id) {
        bool supplierDeleted = await _supplierRepository.SoftDelete(id);
        return supplierDeleted;
    }

    public async Task<List<Supplier>> GetAll() {
        List<Supplier> suppliers = await _supplierRepository.GetAll();
        return suppliers;
    }

    public async Task<Supplier> GetByEmailAndPassword(string email, string password) {
        Expression<Func<Supplier, bool>> filter =
            e => e.Account!.Email.Equals(email) &&
            e.Account.Password.Equals(password);
        Supplier? supplier = await _context.Set<Supplier>()
            .AsNoTracking().Include(e => e.Account)
            .SingleAsync(filter);
        return supplier!;
    }

    public async Task<Supplier> GetById(int id) {
        Supplier supplier = await _supplierRepository.GetByID(id);
        return supplier;
    }

    public async Task<Supply> NewSupply(Supply supply) {
        Supply newSupply = await _supplyRepository.Add(supply);
        //TODO: Update Ingredeitns inventory with the supplied ammount
        throw new NotImplementedException();
    }

    public async Task<Supplier> UpdateAccount(Supplier user) {
        Supplier updatedSupplier = await _supplierRepository.Update(user);
        return updatedSupplier;
    }
}
