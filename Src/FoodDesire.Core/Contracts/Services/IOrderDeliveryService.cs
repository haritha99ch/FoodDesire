namespace FoodDesire.Core.Contracts.Services;
public interface IOrderDeliveryService {
    Task<Order> NewDeliveryForOrder(int orderId, Delivery delivery);
    Task<List<Order>> GetAllOrdersToDeliver();
    Task<List<Order>> GetAllDeliveredOrders();

    Task<Delivery> OrderIsDelivered(int deliveryId);
    Task<Delivery> GetDeliveryById(int deliveryId);
}
