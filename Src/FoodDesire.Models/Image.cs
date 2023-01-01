namespace FoodDesire.Models;
public sealed class Image: Entity {
    [Required, NotNull]
    [Column(TypeName = "varbinary(max)")]
    public string? Data { get; set; }
    public string? Type { get; set; }
}
