namespace FoodDesire.Models;
public sealed class Order: TrackedEntity {
    [Required]
    public int CustomerId { get; set; }
    [Required]
    public DateTime DateTime { get; set; } = DateTime.Now;
    public ICollection<FoodItem>? FoodItems { get; set; }   //TODO: Json column


    [ForeignKey(nameof(CustomerId))]
    public Customer? Customer { get; set; }
}
