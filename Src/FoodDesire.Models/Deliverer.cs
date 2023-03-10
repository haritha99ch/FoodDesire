namespace FoodDesire.Models;
public sealed class Deliverer : Entity {
    [Required]
    public int EmployeeId { get; set; }
    [Required]
    public VehicleType VehicleType { get; set; } = VehicleType.Bike;
    [Required]
    public required string LicenseNo { get; set; }


    [ForeignKey(nameof(EmployeeId))]
    public Employee? Employee { get; set; }
    public ICollection<Delivery>? Deliveries { get; set; }
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