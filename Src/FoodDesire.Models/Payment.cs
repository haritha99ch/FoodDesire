using FoodDesire.Models.Helpers;

namespace FoodDesire.Models;
public sealed class Payment: TrackedEntity {
    [Required, NotNull]
    public PaymentType PaymentType;
    [Required, NotNull]
    public DateTime DateTime { get; private set; } = DateTime.Now;
    [AllowNull]
    public int? OrderId { get; set; }
    [AllowNull]
    public int? EmployeeId {
        get => _employeeId;
        set => PaymentModelHelper.SetEmployee(value, ref _employeeId, ref PaymentType);
    }
    private int? _employeeId;
    [AllowNull]
    public int? SupplyId { get; set; }
    [Required, NotNull]
    public int? ManagedBy { get; set; }
    [Required, NotNull]
    [Column(TypeName = "Decimal(18,2)")]
    public decimal Value { get; set; } = decimal.Zero;


    [ForeignKey(nameof(OrderId))]
    public Order? Order {
        get => _order;
        set => PaymentModelHelper.SetOrder(value!, ref _order!, ref PaymentType);
    }
    private Order? _order;
    [ForeignKey(nameof(EmployeeId))]
    public Employee? Employee { get; set; }
    [ForeignKey(nameof(SupplyId))]
    public Supply? Supply {
        get => _supply;
        set => PaymentModelHelper.SetSupply(value!, ref _supply!, ref PaymentType);
    }
    private Supply? _supply;
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
