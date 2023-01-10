namespace FoodDesire.Models;
public sealed class Recipe: TrackedEntity {
    [Required, NotNull]
    public int ChefId { get; set; } //Created by
    [Required, NotNull]
    public required string Name { get; set; }
    [Required, NotNull]
    public required string Description { get; set; }
    [Required, NotNull]
    [Column(TypeName = "Decimal(18,2)")]
    public decimal MinimumPrice { get; private set; } = decimal.Zero;
    [Required, NotNull]
    [Column(TypeName = "Decimal(18,2)")]
    public decimal FixedPrice { get; set; } = decimal.Zero;
    [Required, NotNull]
    public int ImageId { get; set; }
    [Required, NotNull]
    public int FoodCategoryId { get; set; }
    [NotNull]
    public string Tags { get; set; } = "";


    [ForeignKey(nameof(ChefId))]
    public Chef? Chef { get; set; }
    [ForeignKey(nameof(FoodCategoryId))]
    public RecipeCategory? FoodCategory { get; set; }
    private ICollection<RecipeIngredient>? _recipeIngredients { get; set; }
    public ICollection<RecipeIngredient> RecipeIngredients {
        get {
            return _recipeIngredients!;
        }
        set {
            _recipeIngredients = value;
            value.ToList()
                .ForEach((recipeIngredient) => {
                    MinimumPrice += Convert.ToDecimal(recipeIngredient.Amount * recipeIngredient.Ingredient!.CurrentPricePerUnit);
                });
            FixedPrice = MinimumPrice;
        }
    }
}
