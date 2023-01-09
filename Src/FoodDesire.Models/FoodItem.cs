namespace FoodDesire.Models;
public sealed class FoodItem: TrackedEntity {
    [Required, NotNull]
    public int RecipeId { get; set; }
    private ICollection<FoodItemIngredient>? _foodItemIngredients { get; set; } ////TODO: Json column
    [Required, NotNull]
    public ICollection<FoodItemIngredient>? FoodItemIngredients {
        get => _foodItemIngredients!;
        set {
            _foodItemIngredients = value;
            value?.ToList().ForEach(fi => {
                decimal? pricePerMultiplier = Recipe?.RecipeIngredients?
                    .FirstOrDefault(ri => ri.Id == fi.RecipeIngredientId)?.PricePerMultiplier;
                Price += Convert.ToDecimal(Convert.ToDouble(pricePerMultiplier) * fi.Multiplier);
            });
        }
    }
    public int ChefId { get; set; } //Prepares by
    public int OrderId { get; set; }
    [Column(TypeName = "Decimal(18,2)")]
    public decimal Price { get; private set; } = decimal.Zero;
    //Delete property = food has been prepared
    public FoodItemStatus Status { get; set; } = FoodItemStatus.Queued;

    private Recipe? _recipe { get; set; }
    [ForeignKey(nameof(RecipeId))]
    public Recipe? Recipe {
        get => _recipe;
        set {
            _recipe = value;
            if(FoodItemIngredients != null) return;
            value?.RecipeIngredients.ToList()
                .ForEach(ri => {
                    FoodItemIngredients?.Add(new FoodItemIngredient() {
                        RecipeIngredientId = ri.Id,
                    });
                });
        }
    }
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