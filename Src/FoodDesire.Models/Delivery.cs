namespace FoodDesire.Models;
public sealed class Delivery : Entity {
    [Required]
    public int DelivererId { get; set; }
    [Required]
    public bool IsDelivered { get; set; } = false;
    public Address? Address { get; set; }
    [Required]
    [Column(TypeName = "Decimal(18,2)")]
    public decimal Fee { get; set; } = 100;


    [ForeignKey(nameof(DelivererId))]
    public Deliverer? Deliverer { get; set; }
}
