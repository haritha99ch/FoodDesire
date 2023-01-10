namespace FoodDesire.Models;
public sealed class RecipeCategory: Entity {
    [Required, NotNull]
    public required string Name { get; set; }
    [Required, NotNull]
    public required string Description { get; set; }

    public ICollection<Recipe>? Recipes { get; set; }
}
