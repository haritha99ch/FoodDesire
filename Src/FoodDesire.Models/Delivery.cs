namespace FoodDesire.Models;
public sealed class Delivery: Entity {
    [Required, NotNull]
    public int OrderId { get; set; }
    [Required, NotNull]
    public int DelivererId { get; set; }
    [Required, NotNull]
    public bool IsDelivered { get; set; } = false;
    public Address? Address { get; set; }   ////TODO: Json column


    [ForeignKey(nameof(OrderId))]
    public Order? Order { get; set; }
    [ForeignKey(nameof(DelivererId))]
    public Deliverer? Deliverer { get; set; }
}