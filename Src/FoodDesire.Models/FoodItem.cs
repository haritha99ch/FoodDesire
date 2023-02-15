namespace FoodDesire.Models;
public sealed class FoodItem: TrackedEntity {
    [Required, NotNull]
    public int RecipeId { get; set; }
    [AllowNull]
    public int? OrderId { get; set; }
    public List<FoodItemIngredient> FoodItemIngredients { get; set; } = new List<FoodItemIngredient>();
    [AllowNull]
    public int? ChefId { get; set; } //Prepares by
    [Column(TypeName = "Decimal(18,2)")]
    public decimal Price { get; set; } = decimal.Zero;
    //Delete property = food has been prepared
    public FoodItemStatus Status { get; set; } = FoodItemStatus.Queued;

    [ForeignKey(nameof(RecipeId))]
    public Recipe? Recipe { get; set; }
    [ForeignKey(nameof(ChefId))]
    public Chef? Chef { get; set; }
    [ForeignKey(nameof(OrderId))]
    public Order? Order { get; set; }

}

public enum FoodItemStatus {
    [Display(Name = "Queued")]
    Queued,
    [Display(Name = "Preparing")]
    Preparing,
    [Display(Name = "Prepared")]
    Prepared
}