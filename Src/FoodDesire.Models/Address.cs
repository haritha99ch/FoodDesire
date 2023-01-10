namespace FoodDesire.Models;
public sealed class Address: Entity {
    public string? No { get; set; }
    [Required, NotNull]
    public required string Street1 { get; set; }
    [Required, NotNull]
    public required string Street2 { get; set; }
    [Required, NotNull]
    public required string City { get; set; }
    [Required, NotNull]
    public int PostalCode { get; set; }
}
