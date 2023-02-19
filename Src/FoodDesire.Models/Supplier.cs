namespace FoodDesire.Models;
public sealed class Supplier : Entity {
    [Required, NotNull]
    public int EmployeeId { get; set; }
    [Required, NotNull]
    public string? City { get; set; }

    [ForeignKey(nameof(EmployeeId))]
    public Employee? Employee { get; set; }
}
