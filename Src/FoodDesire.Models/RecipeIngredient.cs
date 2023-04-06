namespace FoodDesire.Models;
[Serializable]
public class RecipeIngredient {
    [AllowNull]
    public int? Recipe_Id { get; set; }
    public string? Recipe_Name { get; set; }
    [AllowNull]
    public int? Ingredient_Id { get; set; }
    public string? Ingredient_Name { get; set; }
    [Required]
    public double Amount { get; set; }
    [Required]
    public double RecommendedAmount { get; set; }
    [Required]
    public Measurement Measurement { get; set; }
    [Required]
    public bool IsRequired { get; set; } = true;
    [Required]
    public bool CanModify { get; set; } = false;
    [Required]
    [Column(TypeName = "Decimal(18,2)")]
    public decimal PricePerMultiplier { get; set; }
    [Column(TypeName = "Decimal(18,2)")]
    public decimal Value { get; set; } = decimal.Zero;
}
