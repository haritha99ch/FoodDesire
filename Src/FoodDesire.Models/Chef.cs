namespace FoodDesire.Models;
public sealed class Chef: Entity {
    [Required, NotNull]
    public int EmployeeId { get; set; }


    [ForeignKey(nameof(EmployeeId))]
    public Employee? Employee { get; set; }
    public List<FoodItem>? FoodItems { get; set; }   //Chef prepares Foods
    public List<Recipe>? Recipes { get; set; }       //Chef creates Recipes
}
