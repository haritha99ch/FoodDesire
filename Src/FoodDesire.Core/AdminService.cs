namespace FoodDesire.Core;
public class AdminService<Admin>: IAdminService<Admin>, IUserService<Admin> where Admin : User {
    private readonly ITrackingRepository<Admin> _adminRepository;
    private readonly FoodDesireContext _context;
    public AdminService(
        FoodDesireContext context,
        ITrackingRepository<Admin> adminRepository
        ) {
        _context = context;
        _adminRepository = adminRepository;
    }

    public async Task<Admin> CreateAccount(Admin user) {
        Admin newAdmin = await _adminRepository.Add(user);
        await _context.SaveChangesAsync();
        return newAdmin;
    }

    public async Task<bool> DeleteAccountById(int id) {
        bool adminDeleted = await _adminRepository.SoftDelete(id);
        return adminDeleted;
    }

    public async Task<Admin> GetByEmailAndPassword(string email, string password) {
        Expression<Func<Admin, bool>> filter =
            e => e.Account!.Email.Equals(email) &&
            e.Account.Password.Equals(password);
        Admin? admin = await _context.Set<Admin>()
            .AsNoTracking().Include(e => e.Account)
            .SingleAsync(filter);
        return admin!;
    }

    public async Task<Admin> GetById(int id) {
        Admin admin = await _adminRepository.GetByID(id);
        return admin;
    }

    public async Task<Admin> UpdateAccount(Admin user) {
        Admin updatedAdmin = await _adminRepository.Update(user);
        return updatedAdmin;
    }

    public Task<List<Admin>> GetAll() {
        throw new NotImplementedException();
    }
}
