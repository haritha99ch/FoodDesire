namespace FoodDesire.Models;
public sealed class RecipeCategory: Entity {
    [Required, NotNull]
    public string? Name { get; set; }
    [Required, NotNull]
    public string? Description { get; set; }

    public ICollection<Recipe>? Recipes { get; set; }
}
