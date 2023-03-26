namespace FoodDesire.Models;
public sealed class Image {
    [Required]
    public required byte[] Data { get; set; }
    public string? Type { get; set; }
}
