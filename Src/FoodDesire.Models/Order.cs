namespace FoodDesire.Models;
public sealed class Order : TrackedEntity {
    [Required]
    public int CustomerId { get; set; }
    [Required]
    public DateTime DateTime { get; set; } = DateTime.Now;
    public OrderStatus Status { get; set; } = OrderStatus.Pending;
    public List<FoodItem>? FoodItems { get; set; }
    [AllowNull]
    public int? DeliveryId { get; set; }
    [AllowNull]
    public int? PaymentId { get; set; }
    public decimal Price { get; set; } = decimal.Zero;

    [ForeignKey(nameof(CustomerId))]
    public Customer? Customer { get; set; }
    [ForeignKey(nameof(DeliveryId))]
    public Delivery? Delivery { get; set; }
    [ForeignKey(nameof(PaymentId))]
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
