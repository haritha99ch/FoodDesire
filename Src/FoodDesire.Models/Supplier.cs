namespace FoodDesire.Models;
public sealed class Supplier : BaseUser {
    [Required]
    public int EmployeeId { get; set; }
    [AllowNull]
    public string? City { get; set; }


    [ForeignKey(nameof(EmployeeId))]
    public Employee Employee { get; set; } = new Employee();
}
