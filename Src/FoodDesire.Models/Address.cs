namespace FoodDesire.Models;
public sealed class Address : Entity {
    public string? No { get; set; }
    [Required]
    public required string Street1 { get; set; }
    [Required]
    public required string Street2 { get; set; }
    [Required]
    public required string City { get; set; }
    [Required]
    public int PostalCode { get; set; }
}
