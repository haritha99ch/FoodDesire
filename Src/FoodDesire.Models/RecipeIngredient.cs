namespace FoodDesire.Models;
public class RecipeIngredient: Entity {
    [AllowNull]
    public int? RecipeId { get; set; }
    [AllowNull]
    public int? IngredientId { get; set; }
    [Required, NotNull]
    public double Amount { get; set; }
    [Required, NotNull]
    public double RecommendedAmount { get; set; }
    [Required, NotNull]
    public bool IsRequired { get; set; } = true;
    [Required]
    public bool CanModify { get; set; } = false;
    [Required, NotNull]
    [Column(TypeName = "Decimal(18,2)")]
    public decimal PricePerMultiplier { get; set; }
    [Required]
    public bool IsRecipe => (RecipeId != null);
}
