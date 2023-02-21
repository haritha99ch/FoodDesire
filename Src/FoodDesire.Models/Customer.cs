namespace FoodDesire.Models;
public sealed class Customer : Entity {
    [Required]
    public int UserId { get; set; }


    [ForeignKey(nameof(UserId))]
    public User? User { get; set; }
    public ICollection<Order>? Orders { get; set; }
}
