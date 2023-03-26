namespace FoodDesire.Models;
public sealed class Employee : Entity {
    [Required]
    public DateTime RegisteredDate { get; set; } = DateTime.Now;

    public ICollection<Payment>? Salaries { get; set; }
}
