namespace FoodDesire.Models;
public sealed class Address: Entity {
    public string? No { get; set; }
    [Required, NotNull]
    public string? Street1 { get; set; }
    [Required, NotNull]
    public string? Street2 { get; set; }
    [Required, NotNull]
    public string? City { get; set; }
    [Required, NotNull]
    public int PostalCode { get; set; }
}
