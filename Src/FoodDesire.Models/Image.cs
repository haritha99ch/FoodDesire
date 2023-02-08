namespace FoodDesire.Models;
public sealed class Image {
    [Required, NotNull]
    [Column(TypeName = "varbinary(max)")]
    public required string Data { get; set; }
    public string? Type { get; set; }
}
