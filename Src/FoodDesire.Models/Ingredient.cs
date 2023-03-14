namespace FoodDesire.Models;
public sealed class Ingredient : TrackedEntity {
    [Required]
    public required string Name { get; set; }
    [Required]
    public required string Description { get; set; }
    [Required]
    public Measurement Measurement { get; set; }
    [Required]
    [Column(TypeName = "Decimal(18,2)")]
    public decimal CurrentPricePerUnit { get; set; } = decimal.Zero;
    [Required]
    public double MaximumQuantity { get; set; }
    [Required]
    public double CurrentQuantity { get; set; }
    [Required]
    public int IngredientCategoryId { get; set; }


    [ForeignKey(nameof(IngredientCategoryId))]
    public IngredientCategory? IngredientCategory { get; set; }
}

public enum Measurement {
    [Display(Name = "mg")]
    Grams,
    [Display(Name = "ml")]
    Liquid,
    [Display(Name = "each")]
    Each
}
