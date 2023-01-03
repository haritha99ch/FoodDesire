namespace FoodDesire.Models;
public sealed class FoodItem: TrackedEntity {
    [Required, NotNull]
    public int RecipeId { get; set; }
    private ICollection<FoodItemIngredient>? _foodItemIngredients { get; set; } ////TODO: Json column
    [Required, NotNull]
    public ICollection<FoodItemIngredient>? FoodItemIngredients {
        get => _foodItemIngredients!;
        set {
            value?.ToList().ForEach(fi => {
                decimal? pricePerMultiplier = Recipe?.RecipeIngredients?
                    .FirstOrDefault(ri => ri.Id == fi.RecipeIngredientId)?.PricePerMultiplier;
                Price += Convert.ToDecimal(Convert.ToDouble(pricePerMultiplier) * fi.Multiplier);
            });
        }
    }
    public int ChefId { get; set; } //Prepares by
    public decimal Price { get; private set; } = decimal.Zero;


    [ForeignKey(nameof(RecipeId))]
    public Recipe? Recipe { get; set; }
    [ForeignKey(nameof(ChefId))]
    public Chef? Chef { get; set; }
}
