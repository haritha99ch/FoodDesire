﻿namespace FoodDesire.Core;
public class OrderService: IOrderService {
    private readonly IRepository<Order> _orderRepository;
    private readonly FoodDesireContext _context;

    public OrderService(IRepository<Order> orderRepository, FoodDesireContext context) {
        _orderRepository = orderRepository;
        _context = context;
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

    public async Task<List<Order>> GetAllDeliveredOrders() {
        Expression<Func<Order, bool>> filter = e => e.Delivery!.IsDelivered;

        List<Order> orders = await _context.Set<Order>()
            .AsNoTracking()
            .Include(e => e.Delivery)
            .Where(filter)
            .ToListAsync();
        return orders;
    }

    public async Task<List<Order>> GetAllOrdersToDeliver() {
        Expression<Func<Order, bool>> filter = e => e.Delivery == null || !e.Delivery.IsDelivered;

        List<Order> orders = await _context.Set<Order>()
            .AsNoTracking()
            .Include(e => e.Delivery)
            .Where(filter)
            .ToListAsync();
        return orders;
    }

    public async Task<bool> DeleteOrderById(int orderId) {
        bool orderDeleted = await _orderRepository.Delete(orderId);
        return orderDeleted;
    }
}