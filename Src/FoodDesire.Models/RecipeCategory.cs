namespace FoodDesire.Models;
public sealed class RecipeCategory : Entity {
    [Required]
    public required string Name { get; set; }
    [Required]
    public required string Description { get; set; }

    public ICollection<Recipe>? Recipes { get; set; }
}
