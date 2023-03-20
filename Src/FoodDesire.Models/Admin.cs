namespace FoodDesire.Models;
public sealed class Admin : BaseUser {
    public ICollection<Payment> Payments { get; set; } = new List<Payment>();
}
