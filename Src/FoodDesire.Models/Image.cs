namespace FoodDesire.Models;
public sealed class Image {
    [Required, NotNull]
    public required string Data { get; set; }
    public string? Type { get; set; }
}
