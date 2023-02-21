namespace FoodDesire.Models;
public sealed class Image {
    [Required]
    public required string Data { get; set; }
    public string? Type { get; set; }
}
