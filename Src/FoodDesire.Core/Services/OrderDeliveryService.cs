namespace FoodDesire.Core.Services;
public class OrderDeliveryService : IOrderDeliveryService {
    private readonly IRepository<Delivery> _deliveryRepository;
    private readonly IRepository<Order> _orderRepository;

    public OrderDeliveryService(
        IRepository<Delivery> deliveryRepository,
        IRepository<Order> orderRepository
        ) {
        _deliveryRepository = deliveryRepository;
        _orderRepository = orderRepository;
    }

    public async Task<Delivery> GetDeliveryById(int deliveryId) {
        Delivery delivery = await _deliveryRepository.GetByID(deliveryId);
        return delivery;
    }

    public async Task<Order> NewDeliveryForOrder(int orderId, Delivery delivery) {
        Order order = await _orderRepository.GetByID(orderId);
        order.Delivery = delivery;
        order.Delivery.Address = delivery.Address ?? order.Customer!.User!.Address;
        order = await _orderRepository.Update(order);
        return order;
    }

    public async Task<List<Order>> GetAllDeliveredOrders() {
        Expression<Func<Order, bool>> filterExpression = e => e.Delivery!.IsDelivered;

        IQueryable<Order> filter(IQueryable<Order> e) => e.Where(filterExpression);
        IIncludableQueryable<Order, object?> include(IQueryable<Order> e) => e.Include(o => o.Delivery);

        List<Order> orders = await _orderRepository.Get(filter, null, include);
        return orders;
    }

    public async Task<List<Order>> GetAllOrdersToDeliver() {
        Expression<Func<Order, bool>> filterExpression = e => e.Delivery == null || !e.Delivery.IsDelivered;

        IQueryable<Order> filter(IQueryable<Order> e) => e.Where(filterExpression);
        IIncludableQueryable<Order, object?> include(IQueryable<Order> e) => e.Include(e => e.Delivery);

        List<Order> orders = await _orderRepository.Get(filter, null, include);
        return orders;
    }

    public async Task<Delivery> OrderIsDelivered(int orderId) {
        Order order = await _orderRepository.GetByID(orderId);
        order.Delivery!.IsDelivered = true;
        order.Status = OrderStatus.Delivered;
        order = await _orderRepository.Update(order);
        return order.Delivery!;
    }
}
