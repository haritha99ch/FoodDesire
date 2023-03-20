namespace FoodDesire.Core.Services;
public class ChefService : IChefService {
    private readonly IRepository<Chef> _chefRepository;
    private readonly ITrackingRepository<User> _userRepository;
    public ChefService(
        IRepository<Chef> chefRepository,
        ITrackingRepository<User> userRepository
        ) {
        _userRepository = userRepository;
        _chefRepository = chefRepository;
    }
    public async Task<Chef> CreateAccount(Chef user) {
        Chef chef = await _chefRepository.Add(user);
        return chef;
    }

    public async Task<bool> DeleteAccountById(int id) {
        Chef chef = await _chefRepository.GetByID(id);
        bool chefDeleted = await _userRepository.SoftDelete(chef!.UserId);
        await _userRepository.SaveChanges();
        return chefDeleted;
    }

    public async Task<List<Chef>> GetAll() {
        List<Chef> chefs = await _chefRepository.GetAll();
        return chefs;
    }

    public async Task<Chef> GetByEmailAndPassword(string email, string password) {
        Expression<Func<Chef, bool>> filter =
            e => e.User!.Account!.Email.Equals(email) && e.User!.Account!.Password!.Equals(password);

        Chef chef = await _chefRepository.GetOne(filter);
        return chef;
    }

    public async Task<Chef> GetById(int id) {
        Chef chef = await _chefRepository.GetByID(id);
        return chef;
    }

    public async Task<Chef> UpdateAccount(Chef user) {
        Chef updatedChef = await _chefRepository.Update(user);
        return updatedChef;
    }

    public async Task<Chef> GetByEmail(string email) {
        Expression<Func<Chef, bool>> filter = e => e!.User!.Account!.Email.Equals(email);

        Chef? chef = await _chefRepository.GetOne(filter);
        return chef!;
    }
}
