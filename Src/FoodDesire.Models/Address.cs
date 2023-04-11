namespace FoodDesire.Models;
public sealed class Address {
    public string? No { get; set; }
    [Required]
    public string? Street1 { get; set; }
    [Required]
    public string? Street2 { get; set; }
    [Required]
    public string? City { get; set; }
    [Required]
    public int PostalCode { get; set; }
}
