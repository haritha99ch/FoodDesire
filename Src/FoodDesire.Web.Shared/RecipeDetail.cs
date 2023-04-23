namespace FoodDesire.Web.Shared;
public class RecipeDetail {
    public int Id { get; set; }
    public string? Name { get; set; }
    public string? Description { get; set; }
    public List<RecipeInstruction> RecipeInstructions { get; set; } = new();
    public List<ImageB64> Images { get; set; } = new();
    public List<RecipeIngredient> RecipeIngredients { get; set; } = new();
    public decimal MinimumPrice { get; set; }
    public decimal FixedPrice { get; set; }
    public int RecipeCategoryId { get; set; }
    public string Tags { get; set; } = "";
    public bool AsIngredient { get; set; } = false;
    public bool IsMenuItem { get; set; } = true;
    public float Rating { get; set; } = 0;
    public int Stars => (int)(Rating * 5);
    public long Times { get; set; } = 0;


    public RecipeCategory? RecipeCategory { get; set; }
}
