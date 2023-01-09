﻿namespace FoodDesire.Core;
public interface IOrderService {
    Task<Order> NewOrder(Order order);
    Task<List<Order>> GetAllOrders();
    Task<Order> GetOrderById(int orderId);
    Task<bool> DeleteOrderById(int orderId);
}
