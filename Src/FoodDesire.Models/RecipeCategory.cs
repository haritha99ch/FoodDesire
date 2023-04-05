namespace FoodDesire.Models;
public sealed class RecipeCategory : Entity {
    [Required]
    public string? Name { get; set; }
    [Required]
    public string? Description { get; set; }
}
