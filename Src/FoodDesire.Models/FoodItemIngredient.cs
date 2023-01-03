namespace FoodDesire.Models;
public sealed class FoodItemIngredient {
    public int RecipeIngredientId { get; set; }
    public double Multiplier { get; set; }
    [Column(TypeName = "Decimal(18,2)")]
    public decimal Price { get; set; }

}