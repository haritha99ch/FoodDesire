namespace FoodDesire.Models;
public sealed class FoodCategory: Entity {
    [Required, NotNull]
    public string? Name { get; set; }
}
