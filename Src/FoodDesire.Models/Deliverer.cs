namespace FoodDesire.Models;
public sealed class Deliverer : BaseUser {
    [Required]
    public int EmployeeId { get; set; }
    [Required]
    public VehicleType VehicleType { get; set; } = VehicleType.Bike;
    [Required]
    public required string LicenseNo { get; set; }


    [ForeignKey(nameof(EmployeeId))]
    public Employee Employee { get; set; } = new Employee();
    public ICollection<Delivery> Deliveries { get; set; } = new List<Delivery>();
}

public enum VehicleType {
    [Display(Name = "Bike")]
    Bike,
    [Display(Name = "Van")]
    Van,
    [Display(Name = "Car")]
    Car,
    [Display(Name = "Thee Wheel")]
    TheeWheel,
}
