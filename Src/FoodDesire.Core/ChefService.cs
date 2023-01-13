namespace FoodDesire.Core;
public class ChefService: IChefService {
    private readonly IRepository<Chef> _chefRepository;
    private readonly ITrackingRepository<User> _userRepository;
    private readonly FoodDesireContext _context;
    public ChefService(
        FoodDesireContext context,
        IRepository<Chef> chefRepository,
        ITrackingRepository<User> userRepository
        ) {
        _context = context;
        _userRepository = userRepository;
        _chefRepository = chefRepository;
    }
    public async Task<Chef> CreateAccount(Chef user) {
        Chef chef = await _chefRepository.Add(user);
        return chef;
    }

    public async Task<bool> DeleteAccountById(int id) {
        Chef chef = await _context.Set<Chef>()
            .AsNoTracking()
            .Include(e => e.Employee)
            .SingleAsync(e => e.Id == id);

        bool chefDeleted = await _userRepository.SoftDelete(chef.Employee!.UserId);
        await _userRepository.SaveChanges();
        return chefDeleted;
    }

    public async Task<List<Chef>> GetAll() {
        List<Chef> chefs = await _chefRepository.GetAll();
        return chefs;
    }

    public async Task<Chef> GetByEmailAndPassword(string email, string password) {
        Expression<Func<Chef, bool>> filter =
            e => e.Employee!.User!.Account!.Email.Equals(email) &&
            e.Employee.User.Account.Password.Equals(password);

        Chef chef = await _context.Set<Chef>()
            .AsNoTracking()
            .Include(e => e.Employee)
            .ThenInclude(e => e!.User)
            .ThenInclude(u => u!.Account)
            .SingleAsync(filter);

        return chef;
    }

    public async Task<Chef> GetByIdPopulated(int id) {
        Chef chef = await _chefRepository.GetOne(e => e.Id == id);
        return chef;
    }

    public async Task<Chef> UpdateAccount(Chef user) {
        Chef updatedChef = await _chefRepository.Update(user);
        await _userRepository.SaveChanges();
        return updatedChef;
    }
}
