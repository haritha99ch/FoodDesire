namespace FoodDesire.Models;
public sealed class Chef : Entity {
    [Required]
    public int EmployeeId { get; set; }
    [Required]
    public int UserId { get; set; }

    [ForeignKey(nameof(UserId))]
    public User? User { get; set; }
    [ForeignKey(nameof(EmployeeId))]
    public Employee? Employee { get; set; } = new Employee();
    public List<FoodItem>? FoodItems { get; set; }   //Chef prepares Foods
    public List<Recipe>? Recipes { get; set; }       //Chef creates Recipes
}
