namespace FoodDesire.Models;
public sealed class Payment : TrackedEntity {
    [Required]
    public PaymentType PaymentType { get; set; }
    [Required]
    public DateTime DateTime { get; private set; } = DateTime.Now;
    [AllowNull]
    public int? OrderId { get; set; }
    [AllowNull]
    public int? EmployeeId { get; set; }
    [AllowNull]
    public int? SupplyId { get; set; }
    [AllowNull]
    public int? ManagedBy { get; set; }
    [Required]
    [Column(TypeName = "Decimal(18,2)")]
    public decimal Value { get; set; } = decimal.Zero;
    public string? Description { get; set; }


    [ForeignKey(nameof(OrderId))]
    public Order? Order { get; set; }
    [ForeignKey(nameof(EmployeeId))]
    public Employee? Employee { get; set; }
    [ForeignKey(nameof(SupplyId))]
    public Supply? Supply { get; set; }
    [ForeignKey(nameof(ManagedBy))]
    public Admin? Admin { get; set; }
}

public enum PaymentType {
    [Display(Name = "Order")]
    Order,
    [Display(Name = "Salary")]
    Salary,
    [Display(Name = "Supplies")]
    Supply,
    [Display(Name = "Maintenance")]
    Maintenance,
}
