namespace FoodDesire.Models;
public sealed class Supplier : Entity {
    [Required]
    public int EmployeeId { get; set; }
    [Required]
    public string? City { get; set; }
    [Required]
    public int UserId { get; set; }


    [ForeignKey(nameof(UserId))]
    public User? User { get; set; }

    [ForeignKey(nameof(EmployeeId))]
    public Employee? Employee { get; set; } = new Employee();
}
