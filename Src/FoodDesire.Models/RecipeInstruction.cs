namespace FoodDesire.Models;
public class RecipeInstruction : Entity {
    [Required]
    public int StepNo { get; set; }
    [Required]
    public required string Step { get; set; }
}
