namespace FoodDesire.Models;
public class RecipeInstruction : Entity {
    [Required]
    public required string Step { get; set; }
}
