namespace FoodDesire.Models;
public class RecipeIngredient : Entity {
    [Required, NotNull]
    public int RecipeId { get; set; }
    [Required, NotNull]
    public int IngredeintId { get; set; }
    [Required, NotNull]
    public double Amount { get; set; }
    [Required, NotNull]
    public double RecommendedAmount { get; set; }
    [Required, NotNull]
    public bool IsRequired { get; set; } = true;
    public int Multiplier { get; set; } = 1;
    public decimal PricePerMultiplier { get; set; }

    [ForeignKey(nameof(RecipeId))]
    public Recipe? Recipe { get; set; }
    [ForeignKey(nameof(IngredeintId))]
    public Ingredient? Ingredient { get; set; }
}
