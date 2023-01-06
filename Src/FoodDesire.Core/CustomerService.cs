namespace FoodDesire.Core;
public class CustomerService<Customer>: IUserService<Customer>, ICustomerService<Customer> where Customer : User {
    private readonly ITrackingRepository<Customer> _customerRepository;
    private readonly ITrackingRepository<Order> _orderRepository;
    private readonly FoodDesireContext _context;
    public CustomerService(
        FoodDesireContext context,
        ITrackingRepository<Customer> customerRepository,
        ITrackingRepository<Order> orderRepository
        ) {
        _context = context;
        _customerRepository = customerRepository;
        _orderRepository = orderRepository;
    }

    public async Task<Customer> CreateAccount(Customer user) {
        Customer newCustomer = await _customerRepository.Add(user);
        await _context.SaveChangesAsync();
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
            e => e.Account!.Email.Equals(email) &&
            e.Account.Password.Equals(password);
        Customer? customer = await _context.Set<Customer>()
            .AsNoTracking().Include(e => e.Account)
            .SingleAsync(filter);
        return customer!;
    }
    public async Task<bool> DeleteAccountById(int id) {
        bool customerDeleted = await _customerRepository.SoftDelete(id);
        return customerDeleted;
    }
    public async Task<Customer> UpdateAccount(Customer user) {
        Customer updatedCustomer = await _customerRepository.Update(user);
        return updatedCustomer;
    }
    public async Task<Order> CreateOrder(Order order) {
        Order newOrder = await _orderRepository.Add(order);
        return newOrder;
    }
    public async Task<Order> AddFoodItemToOrder(int orderId, FoodItem foodItem) {
        Order? order = await _context.Set<Order>()
            .AsNoTracking().Include(e => e.FoodItems)
            .SingleOrDefaultAsync(e => e.Id == orderId);
        if(order == null) return new Order();
        order.FoodItems!.Add(foodItem);
        Order UpdatedOrder = await _orderRepository.Update(order);
        return UpdatedOrder;
    }
    public async Task<Order> RemoveFoodItemFromOrder(int orderId, int foodItemId) {
        Order? order = await _context.Set<Order>()
            .AsNoTracking().Include(e => e.FoodItems)
            .SingleOrDefaultAsync(e => e.Id == orderId)!;
        order!.FoodItems!.Remove(
            order.FoodItems.Single(e => e.Id != foodItemId)!
            );
        Order UpdatedOrder = await _orderRepository.Update(order);
        return UpdatedOrder;
    }
    public async Task<bool> PayForOrder(int orderId) {
        bool orderPayed = await _orderRepository.SoftDelete(orderId);
        //TODO: Implement payment service
        return orderPayed;
    }
}
