namespace FoodDesire.Models;
public sealed class Ingredient: TrackingEntity {
    [Required, NotNull]
    public string? Name { get; set; }
    [Required, NotNull]
    public string? Description { get; set; }
    [Required, NotNull]
    public Measurement Measurement { get; set; }
    [Required, NotNull]
    public double CurrentPricePerUnit { get; set; } //TODO: Automatically update the price when a new supply made: Totalprice/Units
    [Required, NotNull]
    public double QuantityPerUnit { get; set; }
    [Required, NotNull]
    public double MaximumQuantity { get; set; }
    [Required, NotNull]
    public double CurrentQuantity { get; set; }
    [Required, NotNull]
    public int ImageId { get; set; }
    [Required, NotNull]
    public int IngredientCategoryId { get; set; }


    [ForeignKey(nameof(ImageId))]
    public Image? Image { get; set; }
    [ForeignKey(nameof(IngredientCategoryId))]
    public IngredientCategory? IngredientCategory { get; set; }
    public ICollection<Recipe>? Recipes { get; set; }
}

public enum Measurement {
    [Display(Name ="mg")]
    Mass,
    [Display(Name ="ml")]
    Liquid
}