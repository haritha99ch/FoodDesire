namespace FoodDesire.IMS.Core.Services;
public class DeliveriesPageService : IDeliveriesPageService {
    private IOrderDeliveryService _orderDeliveryService;

    public DeliveriesPageService(IOrderDeliveryService orderDeliveryService) {
        _orderDeliveryService = orderDeliveryService;
    }

    public async Task<Order> CompleteDeliveryForOrder(int deliveryId) => await _orderDeliveryService.OrderIsDelivered(deliveryId);

    public async Task<List<Order>> GetMyOrdersToDeliver(int delivererId) => await _orderDeliveryService.GetDelivererOrders(delivererId);

    public async Task<List<Order>> GetOrdersToDeliver() => await _orderDeliveryService.GetAllOrdersToDeliver();

    public async Task<Order> TakeOrderToDeliveryList(int orderId, int delivererId) => await _orderDeliveryService.TakeOrderDelivery(orderId, delivererId);
}
