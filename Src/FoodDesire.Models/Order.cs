namespace FoodDesire.Models;
public sealed class Order: TrackingEntity {
    [Required]
    public int CustomerId { get; set; }
    [Required]
    public DateTime DateTime { get; set; } = DateTime.Now;

    [ForeignKey(nameof(CustomerId))]
    public Customer Customer { get; set; }
    public ICollection<FoodItem>? FoodItems { get; set; }
}
