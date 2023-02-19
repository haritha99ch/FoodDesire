namespace FoodDesire.Models;
public sealed class IngredientCategory : TrackedEntity {
    [Required, NotNull]
    public required string Name { get; set; }
    [Required, NotNull]
    public required string Description { get; set; }

    public ICollection<Ingredient>? Ingredients { get; set; }
}
