namespace FoodDesire.Core.Services;
public class OrderService : IOrderService {
    private readonly IRepository<Order> _orderRepository;

    public OrderService(IRepository<Order> orderRepository) {
        _orderRepository = orderRepository;
    }

    public async Task<Order> NewOrder(Order order) {
        Order newOrder = await _orderRepository.Add(order);
        return newOrder;
    }

    public async Task<Order> GetOrderById(int orderId) {
        Expression<Func<Order, bool>> filterExpression = e => e.Id == orderId;

        IIncludableQueryable<Order, object?> include(IQueryable<Order> e) => e.Include(e => e.Delivery).Include(e => e.Customer);
        IQueryable<Order> filter(IQueryable<Order> e) => e.Where(filterExpression);

        Order order = await _orderRepository.GetOne(filter: filter, include: include);
        return order;
    }

    public async Task<Order> UpdateOrder(Order order) {
        order = await _orderRepository.Update(order);
        return order;
    }

    public async Task<List<Order>> GetAllOrders() {
        List<Order> orders = await _orderRepository.GetAll();
        return orders;
    }

    public async Task<List<Order>> GetRemainingOrders() {
        Expression<Func<Order, bool>> filterExpression = e => e.Status != OrderStatus.Delivered && e.Status != OrderStatus.Pending;

        IIncludableQueryable<Order, object?> include(IQueryable<Order> e) =>
            e.Include(e => e.Delivery).Include(e => e.Customer);
        IQueryable<Order> filter(IQueryable<Order> e) => e.Where(filterExpression);

        List<Order> orders = await _orderRepository.Get(filter, null, include);
        return orders;
    }

    public async Task<bool> DeleteOrderById(int orderId) {
        bool orderDeleted = await _orderRepository.Delete(orderId);
        return orderDeleted;
    }

    public async Task<Order> GetPendingOrderForCustomer(int userId) {
        Expression<Func<Order, bool>> filterExpression = e => e.CustomerId == userId && e.Status == OrderStatus.Pending;
        IQueryable<Order> filter(IQueryable<Order> e) => e.Where(filterExpression);

        IIncludableQueryable<Order, object?> include(IQueryable<Order> e) =>
            e.Include(e => e.Delivery).Include(e => e.Customer);

        Order order = await _orderRepository.GetOne(filter, include);
        return order;
    }

    public async Task<List<Order>> GetAllCustomerOrders(int customerId) {
        Expression<Func<Order, bool>> filterExpression = e => e.CustomerId == customerId;

        IQueryable<Order> filter(IQueryable<Order> e) => e.Where(filterExpression);

        List<Order> orders = await _orderRepository.Get(filter);
        return orders;
    }
}
