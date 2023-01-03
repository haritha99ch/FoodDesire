namespace FoodDesire.Models;
public abstract class Employee: Entity {
    [Required, NotNull]
    public int UserId { get; set; }
    [Required, NotNull]
    public DateTime RegisteredDate { get; set; } = DateTime.Now;


    [ForeignKey(nameof(UserId))]
    public User? User { get; set; }
    public ICollection<Payment>? Salaries { get; set; }
}
