namespace FoodDesire.Core.Services;
public class CustomerService : ICustomerService {
    private readonly IRepository<Customer> _customerRepository;
    private readonly ITrackingRepository<User> _userRepository;
    public CustomerService(
        IRepository<Customer> customerRepository,
        ITrackingRepository<User> userRepository
        ) {
        _customerRepository = customerRepository;
        _userRepository = userRepository;
    }

    public async Task<Customer> CreateAccount(Customer user) {
        Customer newCustomer = await _customerRepository.Add(user);
        return newCustomer;
    }
    public async Task<Customer> GetById(int id) {
        Customer customer = await _customerRepository.GetByID(id);
        return customer;
    }
    public async Task<List<Customer>> GetAll() {
        List<Customer> customers = await _customerRepository.GetAll();
        return customers;
    }
    public async Task<Customer> GetByEmailAndPassword(string email, string password) {
        Expression<Func<Customer, bool>> filter =
            e => e.User!.Account!.Email.Equals(email) && e.User!.Account!.Password.Equals(password);

        Customer? customer = await _customerRepository.GetOne(filter);
        return customer!;
    }
    public async Task<bool> DeleteAccountById(int id) {
        Customer customer = await _customerRepository.GetByID(id);
        bool customerDeleted = await _userRepository.SoftDelete(customer.UserId);
        return customerDeleted;
    }
    public async Task<Customer> UpdateAccount(Customer user) {
        Customer updatedCustomer = await _customerRepository.Update(user);
        return updatedCustomer;
    }
}
