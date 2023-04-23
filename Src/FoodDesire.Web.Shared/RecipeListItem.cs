namespace FoodDesire.Web.Shared;
public class RecipeListItem {
    public int Id { get; set; }
    public required string Name { get; set; }
    public required string Description { get; set; }
    public List<ImageB64> Images { get; set; } = new();
    public decimal FixedPrice { get; set; }
    public int RecipeCategoryId { get; set; }
    public string Tags { get; set; } = "";
    public float Rating { get; set; } = 0;
    public int Stars => (int)(Rating * 5);
    public long Times { get; set; } = 0;
    public RecipeCategory? RecipeCategory { get; set; }
}

public class ImageB64 {
    public required string Data { get; set; }
}
