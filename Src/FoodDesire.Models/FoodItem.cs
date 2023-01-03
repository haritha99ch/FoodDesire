namespace FoodDesire.Models;
public sealed class FoodItem: TrackedEntity {
    [Required, NotNull]
    public int RecipeId { get; set; }
    [Required, NotNull]
    public ICollection<FoodItemIngredient>? FoodItemIngredients { get; set; } ////TODO: Json column
    public int ChefId { get; set; } //Prepares by


    [ForeignKey(nameof(RecipeId))]
    public Recipe? Recipe { get; set; }
    [ForeignKey(nameof(ChefId))]
    public Chef? Chef { get; set; }
}
