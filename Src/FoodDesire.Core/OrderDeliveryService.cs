namespace FoodDesire.Core;
public class OrderDeliveryService: IOrderDeliveryService {
    private readonly IRepository<Delivery> _deliveryRepository;

    public OrderDeliveryService(IRepository<Delivery> deliveryRepository) {
        _deliveryRepository = deliveryRepository;
    }

    public async Task<Delivery> GetDeliveryById(int deliveryId) {
        Delivery delivery = await _deliveryRepository.GetByID(deliveryId);
        return delivery;
    }

    public async Task<Delivery> NewDeliveryForOrder(Delivery delivery) {
        Delivery newDelivery = await _deliveryRepository.Add(delivery);
        return newDelivery;
    }

    public async Task<Delivery> OrderIsDelivered(int deliveryId) {
        Delivery delivery = await _deliveryRepository.GetByID(deliveryId);
        delivery.IsDelivered = true;
        delivery = await _deliveryRepository.Update(delivery);
        return delivery;
    }
}
