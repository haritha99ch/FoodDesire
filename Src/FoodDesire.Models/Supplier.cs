namespace FoodDesire.Models;
public sealed class Supplier : Entity {
    [Required]
    public int EmployeeId { get; set; }
    [Required]
    public string? City { get; set; }

    [ForeignKey(nameof(EmployeeId))]
    public Employee? Employee { get; set; }
}
