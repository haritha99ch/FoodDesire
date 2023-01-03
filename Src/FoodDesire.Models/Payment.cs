namespace FoodDesire.Models;
public sealed class Payment: Entity {
    [Required, NotNull]
    public PaymentType PaymentType { get; private set; }
    [Required, NotNull]
    public DateTime DateTime { get; set; } = DateTime.Now;
    [AllowNull]
    public int? OrderId {
        get => _orderId;
        set {
            PaymentType = PaymentType.Order;
            _orderId = value;
        }
    }
    private int? _orderId;

    [AllowNull]
    public int? EmployeeId {
        get => _employeeId;
        set {
            PaymentType = PaymentType.Salary;
            _employeeId = value;
        }
    }
    public int? _employeeId;

    [AllowNull]
    public int SupplyId {
        get => _supplyId;
        set {
            PaymentType = PaymentType.Supply;
            _supplyId = value;
        }
    }
    private int _supplyId;

    [Required, NotNull]
    public int? ManagedBy { get; set; }
    [Required, NotNull]
    public decimal Value { get; set; } = decimal.Zero;


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
