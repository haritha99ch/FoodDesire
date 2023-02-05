namespace FoodDesire.Models;
public sealed class Payment: TrackedEntity {
    [Required, NotNull]
    public PaymentType PaymentType { get; private set; }
    [Required, NotNull]
    public DateTime DateTime { get; private set; } = DateTime.Now;
    [AllowNull]

    private int? _orderId;

    [AllowNull]
    public int? EmployeeId {
        get => _employeeId;
        set {
            _employeeId = value;
            if(value == null) return;
            PaymentType = PaymentType.Salary;
        }
    }
    public int? _employeeId;

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
        set {
            _order = value;
            if(value == null) return;
            PaymentType = PaymentType.Order;
        }
    }
    private Order? _order;
    [ForeignKey(nameof(EmployeeId))]
    public Employee? Employee { get; set; }
    [ForeignKey(nameof(SupplyId))]
    public Supply? Supply {
        get => _supply;
        set {
            _supply = value;
            if(value == null) return;
            PaymentType = PaymentType.Supply;
        }
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
