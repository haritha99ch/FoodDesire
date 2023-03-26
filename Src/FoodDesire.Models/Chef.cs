namespace FoodDesire.Models;
public sealed class Chef : BaseUser {
    [Required]
    public int EmployeeId { get; set; }


    [ForeignKey(nameof(EmployeeId))]
    public Employee Employee { get; set; } = new Employee();
    public List<Recipe> Recipes { get; set; } = new List<Recipe>();       //Chef creates Recipes
}
