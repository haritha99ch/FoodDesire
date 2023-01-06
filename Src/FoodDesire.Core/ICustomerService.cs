namespace FoodDesire.Core;
public interface ICustomerService<Customer>: IUserService<Customer> where Customer : User {
    Task<Order> CreateOrder(Order order);
    Task<Order> AddFoodItemToOrder(int orderId, FoodItem foodItem);
    Task<Order> RemoveFoodItemFromOrder(int orderId, int foodItemId);
    Task<bool> PayForOrder(int orderId);
}
