namespace FoodDesire.Web.Shared;
public class FoodItemListItem {
    public int Id { get; set; }
    public int RecipeId { get; set; }
    public int? OrderId { get; set; }
    public List<FoodItemIngredient> FoodItemIngredients { get; set; } = new List<FoodItemIngredient>();
    public int? ChefId { get; set; }
    public decimal Price { get; set; } = decimal.Zero;
    public int Quantity { get; set; } = 1;
    public FoodItemStatus Status { get; set; } = FoodItemStatus.Queued;

    public RecipeDetail? Recipe { get; set; }
}
