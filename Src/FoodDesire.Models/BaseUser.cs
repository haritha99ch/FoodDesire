namespace FoodDesire.Models;
public class BaseUser : Entity {
    [Required]
    public int UserId { get; set; }

    [ForeignKey(nameof(UserId))]
    public User? User { get; set; }
}
