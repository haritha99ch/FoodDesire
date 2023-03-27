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
        Expression<Func<Order, bool>> filterExpression = e => e.CustomerId == customerId;
        Expression<Func<Order, object>> orderExpression = e => e.DateTime;

        IQueryable<Order> filter(IQueryable<Order> e) => e.Where(filterExpression);
        IOrderedQueryable<Order> order(IQueryable<Order> e) => e.OrderBy(orderExpression);

        List<Order> orders = await _orderRepository.Get(filter, order);
        return orders;
    }

    public async Task<List<Order>> GetAllOrdersToDeliverForCustomerById(int customerId) {
        Expression<Func<Order, bool>> filterExpression = e => e.CustomerId == customerId && e.Status == OrderStatus.Prepared;
        Expression<Func<Order, object>> orderExpression = e => e.DateTime;

        IQueryable<Order> filter(IQueryable<Order> e) => e.Where(filterExpression);
        IOrderedQueryable<Order> order(IQueryable<Order> e) => e.OrderBy(orderExpression);

        List<Order> orders = await _orderRepository.Get(filter, order);
        return orders;
    }

    public async Task<List<Order>> GetAllPendingOrdersForCustomerById(int customerId) {
        Expression<Func<Order, bool>> filterExpression = e => e.CustomerId == customerId && e.Status == OrderStatus.Pending;
        Expression<Func<Order, object>> orderExpression = e => e.DateTime;

        IQueryable<Order> filter(IQueryable<Order> e) => e.Where(filterExpression);
        IOrderedQueryable<Order> order(IQueryable<Order> e) => e.OrderBy(orderExpression);

        List<Order> orders = await _orderRepository.Get(filter, order);
        return orders;
    }
}
