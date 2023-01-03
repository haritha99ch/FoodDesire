namespace FoodDesire.Models;
public sealed class FoodItem {
    public int RecipeId { get; set; }
    public int OrderId { get; set; }
    public ICollection<RecipeIngredient>? Ingredients { get; set; }

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
}
