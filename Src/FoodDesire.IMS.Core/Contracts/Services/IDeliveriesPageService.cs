namespace FoodDesire.IMS.Core.Contracts.Services;
public interface IDeliveriesPageService {
    Task<Order> TakeOrderToDeliveryList(int orderId, int delivererId);
    Task<Order> CompleteDeliveryForOrder(int deliveryId);
    Task<List<Order>> GetOrdersToDeliver();
    Task<List<Order>> GetMyOrdersToDeliver(int delivererId);
}
