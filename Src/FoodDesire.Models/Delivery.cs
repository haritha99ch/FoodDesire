namespace FoodDesire.Models;
public sealed class Delivery : Entity {
    [Required]
    public int OrderId { get; set; }
    [Required]
    public int DelivererId { get; set; }
    [Required]
    public bool IsDelivered { get; set; } = false;
    public Address? Address { get; set; }


    [ForeignKey(nameof(OrderId))]
    public Order? Order { get; set; }
    [ForeignKey(nameof(DelivererId))]
    public Deliverer? Deliverer { get; set; }
}