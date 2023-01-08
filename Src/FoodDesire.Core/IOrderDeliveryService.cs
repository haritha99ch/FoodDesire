namespace FoodDesire.Core;
public interface IOrderDeliveryService {
    Task<Delivery> NewDeliveryForOrder(Delivery delivery);
    Task<Delivery> OrderIsDelivered(int deliveryId);
    Task<Delivery> GetDeliveryById(int deliveryId);
}
