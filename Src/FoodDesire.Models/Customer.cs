namespace FoodDesire.Models;
public sealed class Customer : BaseUser {
    public List<Order> Orders { get; set; } = new();
}
