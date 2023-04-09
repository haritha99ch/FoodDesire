namespace FoodDesire.Models;
public class RecipeReview : Entity {
    [Required]
    public int CustomerId { get; set; }
    [Required]
    public int RecipeId { get; set; }
    public string? FeedBack { get; set; }
    [Required]
    [Range(0, 1)]
    public float Rating { get; set; }

    [ForeignKey(nameof(CustomerId))]
    public Customer? Customer { get; set; }
    [ForeignKey(nameof(RecipeId))]
    public Recipe? Recipe { get; set; }
}
