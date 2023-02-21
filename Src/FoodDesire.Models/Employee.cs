namespace FoodDesire.Models;
public sealed class Employee : Entity {
    [Required]
    public int UserId { get; set; }
    [Required]
    public DateTime RegisteredDate { get; set; } = DateTime.Now;


    [ForeignKey(nameof(UserId))]
    public User? User { get; set; }
    public ICollection<Payment>? Salaries { get; set; }
}
