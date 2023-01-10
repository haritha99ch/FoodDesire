namespace FoodDesire.Models;
public sealed class IngredientCategory: TrackedEntity {
    [Required, NotNull]
    public string? Name { get; set; }

    public ICollection<Ingredient>? Ingredients { get; set; }
}
