namespace FoodDesire.Models;
public sealed class Chef: Entity {
    [Required, NotNull]
    public int EmployeeId { get; set; }


    [ForeignKey(nameof(EmployeeId))]
    public Employee? Employee { get; set; }
    public ICollection<FoodItem>? FoodItems { get; set; }   //Chef prepares Foods
    public ICollection<Recipe>? Recipes { get; set; }       //Chef creates Recipes
}
