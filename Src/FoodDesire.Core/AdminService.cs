namespace FoodDesire.Core;
public class AdminService: IAdminService {
    private readonly IRepository<Admin> _adminRepository;
    private readonly ITrackingRepository<User> _userRepository;
    private readonly FoodDesireContext _context;
    public AdminService(
        FoodDesireContext context,
        IRepository<Admin> adminRepository,
        ITrackingRepository<User> userRepository
        ) {
        _context = context;
        _adminRepository = adminRepository;
        _userRepository = userRepository;
    }

    public async Task<Admin> CreateAccount(Admin user) {
        Admin newAdmin = await _adminRepository.Add(user);
        await _context.SaveChangesAsync();
        return newAdmin;
    }

    public async Task<bool> DeleteAccountById(int id) {
        Admin admin = await _adminRepository.GetByID(id);
        bool adminDeleted = await _userRepository.SoftDelete(admin.UserId);
        return adminDeleted;
    }

    public async Task<Admin> GetByEmailAndPassword(string email, string password) {
        Expression<Func<Admin, bool>> filter =
            e => e.User!.Account!.Email.Equals(email) &&
            e.User!.Account.Password.Equals(password);
        Admin? admin = await _context.Set<Admin>()
            .AsNoTracking()
            .Include(e => e.User)
            .ThenInclude(u => u!.Account)
            .SingleAsync(filter);
        return admin!;
    }

    public async Task<Admin> GetByIdPopulated(int id) {
        Admin admin = await _context.Set<Admin>()
            .AsNoTracking()
            .Include(e => e.User)
            .ThenInclude(u => u!.Account)
            .SingleAsync(e => e.Id == id);
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
