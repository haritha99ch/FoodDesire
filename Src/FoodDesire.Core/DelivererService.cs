namespace FoodDesire.Core;
public class DelivererService: IDelivererService {
    private readonly IRepository<Deliverer> _delivererRepository;
    private readonly TrackingRepository<User> _userRepository;
    private readonly FoodDesireContext _context;

    public DelivererService(
        IRepository<Deliverer> delivererRepository,
        TrackingRepository<User> userRepository,
        FoodDesireContext foodDesireContext
        ) {
        _delivererRepository = delivererRepository;
        _userRepository = userRepository;
        _context = foodDesireContext;
    }

    public async Task<Deliverer> CreateAccount(Deliverer user) {
        Deliverer deliverer = await _delivererRepository.Add(user);
        return deliverer;
    }

    public async Task<bool> DeleteAccountById(int id) {
        Deliverer deliverer = await _context.Set<Deliverer>()
            .AsNoTracking()
            .Include(e => e.Employee)
            .ThenInclude(e => e!.User)
            .SingleAsync(e => e.Id == id);

        bool delivererDeleted = await _userRepository.SoftDelete(deliverer.Employee!.UserId);
        return delivererDeleted;
    }

    public async Task<List<Deliverer>> GetAll() {
        List<Deliverer> deliverers = await _delivererRepository.GetAll();
        return deliverers;
    }

    public async Task<Deliverer> GetByEmailAndPassword(string email, string password) {
        Expression<Func<Deliverer, bool>> filter =
            e => e.Employee!.User!.Account!.Email.Equals(email) &&
            e.Employee.User.Account.Password.Equals(password);

        Deliverer deliverer = await _context.Set<Deliverer>()
            .AsNoTracking()
            .Include(e => e.Employee)
            .ThenInclude(e => e!.User)
            .ThenInclude(u => u!.Account)
            .SingleAsync(filter);

        return deliverer;
    }

    public async Task<Deliverer> GetByIdPopulated(int id) {
        Deliverer deliverer = await _context.Set<Deliverer>()
            .AsNoTracking()
            .Include(e => e.Employee)
            .ThenInclude(e => e!.User)
            .ThenInclude(u => u!.Account)
            .Include(e => e.Employee!.User!.Address)
            .SingleAsync(e => e.Id == id);

        return deliverer;
    }

    public async Task<Deliverer> UpdateAccount(Deliverer user) {
        Deliverer updatedDeliverer = await _delivererRepository.Update(user);
        return updatedDeliverer;
    }
}
