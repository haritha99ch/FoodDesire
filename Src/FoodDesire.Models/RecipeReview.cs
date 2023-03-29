namespace FoodDesire.Models;
public class RecipeReview : Entity {
    public string? Review { get; set; }
    [Required]
    [Range(0, 5)]
    public required int Rate { get; set; }
}
