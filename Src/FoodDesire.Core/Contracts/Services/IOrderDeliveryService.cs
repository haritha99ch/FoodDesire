namespace FoodDesire.Core.Contracts.Services;
public interface IOrderDeliveryService {
    Task<Order> NewDeliveryForOrder(int orderId, Delivery delivery);
    Task<List<Order>> GetAllOrdersToDeliver();
    Task<List<Order>> GetAllDeliveredOrders();
    Task<Order> TakeOrderDelivery(int orderId, int delivererId);
    Task<Order> OrderIsDelivered(int deliveryId);
    Task<Delivery> GetDeliveryById(int deliveryId);
    Task<List<Order>> GetDelivererOrders(int deliveryId);
}
