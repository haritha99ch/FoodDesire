namespace FoodDesire.Models;
public class RecipeReview : Entity {
    [Required]
    public int CustomerId { get; set; }
    [Required]
    public int RecipeId { get; set; }
    [Display(Name = "Feed Back")]
    public string? FeedBack { get; set; }
    [Required]
    public float Rating { get; set; }
    [AllowNull]
    public DateTime DateTime { get; set; } = DateTime.Now;

    [ForeignKey(nameof(CustomerId))]
    public Customer? Customer { get; set; }
    [ForeignKey(nameof(RecipeId))]
    public Recipe? Recipe { get; set; }
}
