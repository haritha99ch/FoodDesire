namespace FoodDesire.Models;
public sealed class Ingredient : TrackedEntity {
    [Required]
    public required string Name { get; set; }
    [Required]
    public required string Description { get; set; }
    [Required]
    public Measurement Measurement { get; set; }
    [Required]
    public double CurrentPricePerUnit { get; set; } //TODO: Automatically update the price when a new supply made: Totalprice/Units
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