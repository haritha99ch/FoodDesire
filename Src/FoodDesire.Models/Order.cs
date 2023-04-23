namespace FoodDesire.Models;
public sealed class Order : TrackedEntity {
    [Required]
    public int CustomerId { get; set; }
    [AllowNull]
    public int? DeliveryId { get; set; }
    [Required]
    public DateTime DateTime { get; set; } = DateTime.Now;
    public OrderStatus Status { get; set; } = OrderStatus.Pending;
    [Column(TypeName = "Decimal(18,2)")]
    public decimal Price { get; set; } = decimal.Zero;

    [ForeignKey(nameof(CustomerId))]
    public Customer? Customer { get; set; }
    [ForeignKey(nameof(DeliveryId))]
    public Delivery? Delivery { get; set; }
    public Payment? Payment { get; set; }
}

public enum OrderStatus {
    [Display(Name = "Pending")]
    Pending,
    [Display(Name = "Ordered")]
    Ordered,
    [Display(Name = "Preparing")]
    Preparing,
    [Display(Name = "Prepared")]
    Prepared,
    [Display(Name = "Delivered")]
    Delivered,
}
