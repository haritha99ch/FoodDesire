namespace FoodDesire.IMS.Core.Services;
public class DeliveriesPageService : IDeliveriesPageService {
    private IOrderDeliveryService _orderDeliveryService;
    private IFoodItemService _foodItemService;

    public DeliveriesPageService(IOrderDeliveryService orderDeliveryService, IFoodItemService foodItemService) {
        _orderDeliveryService = orderDeliveryService;
        _foodItemService = foodItemService;
    }

    public async Task<Order> CompleteDeliveryForOrder(int deliveryId) => await _orderDeliveryService.OrderIsDelivered(deliveryId);

    public async Task<List<FoodItem>> GetFoodItemsForSelectedOrder(int orderId) => await _foodItemService.GetAllFoodItemsForOrder(orderId);

    public async Task<List<Order>> GetMyOrdersToDeliver(int delivererId) => await _orderDeliveryService.GetDelivererOrders(delivererId);

    public async Task<List<Order>> GetOrdersToDeliver() => await _orderDeliveryService.GetAllOrdersToDeliver();

    public async Task<Order> TakeOrderToDeliveryList(int orderId, int delivererId) => await _orderDeliveryService.TakeOrderDelivery(orderId, delivererId);
}
