namespace FoodDesire.Models;
public sealed class Recipe : TrackedEntity {
    [Required]
    public int ChefId { get; set; } //Created by
    [Required]
    public required string Name { get; set; }
    [Required]
    public required string Description { get; set; }
    [Required]
    [Column(TypeName = "Decimal(18,2)")]
    public decimal MinimumPrice { get; set; }
    [Required]
    [Column(TypeName = "Decimal(18,2)")]
    public decimal FixedPrice { get; set; }
    [Required]
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
