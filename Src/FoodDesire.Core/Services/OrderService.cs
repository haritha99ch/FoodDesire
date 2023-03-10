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
        Order order = await _orderRepository.GetByID(orderId);
        return order;
    }

    public async Task<List<Order>> GetAllOrders() {
        List<Order> orders = await _orderRepository.GetAll();
        return orders;
    }

    public async Task<bool> DeleteOrderById(int orderId) {
        bool orderDeleted = await _orderRepository.Delete(orderId);
        return orderDeleted;
    }
}
