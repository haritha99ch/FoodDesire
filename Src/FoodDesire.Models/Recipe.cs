namespace FoodDesire.Models;
public class Recipe : TrackedEntity {
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
    public List<Image> Images { get; set; } = new();


    [ForeignKey(nameof(ChefId))]
    public Chef? Chef { get; set; }
    [ForeignKey(nameof(RecipeCategoryId))]
    public RecipeCategory? RecipeCategory { get; set; }
    [NotMapped]
    public List<RecipeIngredient> RecipeIngredients { get; set; } = new();
}
