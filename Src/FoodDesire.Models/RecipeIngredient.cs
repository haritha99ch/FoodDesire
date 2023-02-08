namespace FoodDesire.Models;
public class RecipeIngredient: Entity {
    [Required, NotNull]
    public int RecipeId { get; set; }
    [Required, NotNull]
    public int IngredientId { get; set; }
    [Required, NotNull]
    public double Amount { get; set; }
    [Required, NotNull]
    public double RecommendedAmount { get; set; }
    [Required, NotNull]
    public bool IsRequired { get; set; } = true;
    [Required, NotNull]
    [Column(TypeName = "Decimal(18,2)")]
    public decimal PricePerMultiplier { get; set; }

    //[ForeignKey(nameof(RecipeId))]
    //public Recipe? Recipe { get; set; }
    //[ForeignKey(nameof(IngredientId))]
    //public Ingredient? Ingredient { get; set; }
}
