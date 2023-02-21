namespace FoodDesire.Models;
public sealed class Admin : Entity {
    [Required]
    public int UserId { get; set; }
    public ICollection<Payment>? Payments { get; set; } //Manages by


    [ForeignKey(nameof(UserId))]
    public User? User { get; set; }
}
