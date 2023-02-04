using FoodDesire.Core.Contracts.Services;
using FoodDesire.DAL.Context;
using FoodDesire.DAL.Contracts.Repositories;

namespace FoodDesire.Core.Services;
public class OrderDeliveryService : IOrderDeliveryService
{
    private readonly IRepository<Delivery> _deliveryRepository;
    private readonly FoodDesireContext _context;

    public OrderDeliveryService(
        IRepository<Delivery> deliveryRepository,
        FoodDesireContext context
        )
    {
        _deliveryRepository = deliveryRepository;
        _context = context;
    }

    public async Task<Delivery> GetDeliveryById(int deliveryId)
    {
        Delivery delivery = await _deliveryRepository.GetByID(deliveryId);
        return delivery;
    }

    public async Task<Delivery> NewDeliveryForOrder(Delivery delivery)
    {
        Delivery newDelivery = await _deliveryRepository.Add(delivery);
        return newDelivery;
    }

    public async Task<List<Order>> GetAllDeliveredOrders()
    {
        Expression<Func<Order, bool>> filter = e => e.Delivery!.IsDelivered;

        List<Order> orders = await _context.Set<Order>()
            .AsNoTracking()
            .Include(e => e.Delivery)
            .Where(filter)
            .ToListAsync();
        return orders;
    }

    public async Task<List<Order>> GetAllOrdersToDeliver()
    {
        Expression<Func<Order, bool>> filter = e => e.Delivery == null || !e.Delivery.IsDelivered;

        List<Order> orders = await _context.Set<Order>()
            .AsNoTracking()
            .Include(e => e.Delivery)
            .Where(filter)
            .ToListAsync();
        return orders;
    }


    public async Task<Delivery> OrderIsDelivered(int deliveryId)
    {
        Delivery delivery = await _deliveryRepository.GetByID(deliveryId);
        delivery.IsDelivered = true;
        delivery = await _deliveryRepository.Update(delivery);
        return delivery;
    }
}
