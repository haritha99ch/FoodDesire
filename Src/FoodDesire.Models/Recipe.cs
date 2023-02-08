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
    public decimal MinimumPrice { get; set; }
    [Required, NotNull]
    [Column(TypeName = "Decimal(18,2)")]
    public decimal FixedPrice { get; set; }
    [Required, NotNull]
    public int RecipeCategoryId { get; set; }
    [NotNull]
    public string Tags { get; set; } = "";


    [ForeignKey(nameof(ChefId))]
    public Chef? Chef { get; set; }
    [ForeignKey(nameof(RecipeCategoryId))]
    public RecipeCategory? FoodCategory { get; set; }
    [NotMapped]
    public List<RecipeIngredient> RecipeIngredients { get; set; } = new List<RecipeIngredient>();
    [NotMapped]
    public Image? Image { get; set; }
}
