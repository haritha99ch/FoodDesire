namespace FoodDesire.Models;
public sealed class Customer: Entity {
    [Required, NotNull]
    public int UserId { get; set; }


    [ForeignKey(nameof(UserId))]
    public User? User { get; set; }
    public ICollection<Order>? Orders { get; set; }
}
