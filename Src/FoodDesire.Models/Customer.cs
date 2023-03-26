namespace FoodDesire.Models;
public sealed class Customer : BaseUser {
    public ICollection<Order> Orders { get; set; } = new List<Order>();
}
