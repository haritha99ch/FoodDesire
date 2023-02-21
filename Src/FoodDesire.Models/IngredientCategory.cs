namespace FoodDesire.Models;
public sealed class IngredientCategory : TrackedEntity {
    [Required]
    public required string Name { get; set; }
    [Required]
    public required string Description { get; set; }

    public ICollection<Ingredient>? Ingredients { get; set; }
}
