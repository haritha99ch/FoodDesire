namespace FoodDesire.Core.Services;
public class DelivererService: IDelivererService {
    private readonly IRepository<Deliverer> _delivererRepository;
    private readonly ITrackingRepository<User> _userRepository;

    public DelivererService(
        IRepository<Deliverer> delivererRepository,
        ITrackingRepository<User> userRepository
        ) {
        _delivererRepository = delivererRepository;
        _userRepository = userRepository;
    }

    public async Task<Deliverer> CreateAccount(Deliverer user) {
        Deliverer deliverer = await _delivererRepository.Add(user);
        return deliverer;
    }

    public async Task<bool> DeleteAccountById(int id) {
        Deliverer deliverer = await _delivererRepository.GetByID(id);
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

        Deliverer deliverer = await _delivererRepository.GetOne(filter);
        return deliverer;
    }

    public async Task<Deliverer> GetByIdPopulated(int id) {
        Deliverer deliverer = await _delivererRepository.GetByID(id);
        return deliverer;
    }

    public async Task<Deliverer> UpdateAccount(Deliverer user) {
        Deliverer updatedDeliverer = await _delivererRepository.Update(user);
        return updatedDeliverer;
    }
}
