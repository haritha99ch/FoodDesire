using System.Text.Json.Serialization;

namespace FoodDesire.Models;
public sealed class Image : Entity {
    [Required]
    public required byte[] Data { get; set; }
    public int RecipeId { get; set; }
    public string? Type { get; set; }

    [ForeignKey(nameof(RecipeId))]
    [JsonIgnore]
    public Recipe? Recipe { get; set; }
}
