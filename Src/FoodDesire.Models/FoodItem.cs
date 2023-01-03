namespace FoodDesire.Models;
public sealed class FoodItem {
    [Required, NotNull]
    public int RecipeId { get; set; }
    [Required, NotNull]
    public int OrderId { get; set; }
    [Required, NotNull]
    public ICollection<RecipeIngredient>? Ingredients { get; set; } ////TODO: Json column
    public int ChefId { get; set; } //Prepares by


    private Recipe? _recipe { get; set; }
    [ForeignKey(nameof(RecipeId))]
    public Recipe? Recipe {
        get { return _recipe; }
        set {
            _recipe = value;
            Ingredients = value!.RecipeIngredients;
        }
    }
    [ForeignKey(nameof(OrderId))]
    public Order? Order { get; set; }
    [ForeignKey(nameof(ChefId))]
    public Chef? Chef { get; set; }
}
