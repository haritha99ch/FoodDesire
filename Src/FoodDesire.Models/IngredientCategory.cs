namespace FoodDesire.Models;
public sealed class IngredientCategory : TrackedEntity {
    [Required]
    public required string Name { get; set; }
    [Required]
    public required string Description { get; set; }

    public List<Ingredient> Ingredients { get; set; } = new();
}
