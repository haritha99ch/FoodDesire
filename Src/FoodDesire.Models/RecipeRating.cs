namespace FoodDesire.Models;
public class RecipeRating : Entity {
    [Required]
    public int CustomerId { get; set; }
    public string? FeedBack { get; set; }
    [Required]
    [Range(0, 1)]
    public float Rating { get; set; }

    [ForeignKey(nameof(CustomerId))]
    public Customer? Customer { get; set; }
}
