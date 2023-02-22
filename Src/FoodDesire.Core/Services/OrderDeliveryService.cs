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

    public async Task<Delivery> NewDeliveryForOrder(Delivery delivery) {
        Order order = await _orderRepository.GetByID(delivery.OrderId);
        order.Delivery = delivery;
        order.Delivery.Address = (delivery.Address == null) ? order.Customer!.User!.Address : delivery.Address;
        order = await _orderRepository.Update(order);
        return order.Delivery!;
    }

    public async Task<List<Order>> GetAllDeliveredOrders() {
        Expression<Func<Order, bool>> filter = e => e.Delivery!.IsDelivered;
        Func<IQueryable<Order>, IIncludableQueryable<Order, object?>> include = e => e.Include(o => o.Delivery);

        List<Order> orders = await _orderRepository.Get(filter, null, include);
        return orders;
    }

    public async Task<List<Order>> GetAllOrdersToDeliver() {
        Expression<Func<Order, bool>> filter = e => e.Delivery == null || !e.Delivery.IsDelivered;
        Func<IQueryable<Order>, IIncludableQueryable<Order, object?>> include = e => e.Include(e => e.Delivery);

        List<Order> orders = await _orderRepository.Get(filter, null, include);
        return orders;
    }


    public async Task<Delivery> OrderIsDelivered(int deliveryId) {
        Delivery delivery = await _deliveryRepository.GetByID(deliveryId);
        delivery.IsDelivered = true;
        delivery = await _deliveryRepository.Update(delivery);
        return delivery;
    }
}
