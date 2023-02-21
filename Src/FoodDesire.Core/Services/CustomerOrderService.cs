namespace FoodDesire.Core.Services;
public class CustomerOrderService : ICustomerOrderService {
    private readonly IRepository<Customer> _customerRepository;
    private readonly IRepository<Order> _orderRepository;

    public CustomerOrderService(
        IRepository<Customer> customerRepository,
        IRepository<Order> orderRepository
        ) {
        _customerRepository = customerRepository;
        _orderRepository = orderRepository;
    }

    public async Task<List<Order>> GetAllOrdersForCustomerById(int customerId) {
        Expression<Func<Order, bool>> filter = e => e.CustomerId == customerId;
        Expression<Func<Order, object>> order = e => e.DateTime;

        List<Order> orders = await _orderRepository.Get(filter, order);
        return orders;
    }

    public async Task<List<Order>> GetAllOrdersToDeliverForCustomerById(int customerId) {
        Expression<Func<Order, bool>> filter = e => e.CustomerId == customerId && e.Status == OrderStatus.Prepared;
        Expression<Func<Order, object>> order = e => e.DateTime;

        List<Order> orders = await _orderRepository.Get(filter, order);
        return orders;
    }

    public async Task<List<Order>> GetAllPendingOrdersForCustomerById(int customerId) {
        Expression<Func<Order, bool>> filter = e => e.CustomerId == customerId && e.Status == OrderStatus.Pending;
        Expression<Func<Order, object>> order = e => e.DateTime;

        List<Order> orders = await _orderRepository.Get(filter, order);
        return orders;
    }
}
