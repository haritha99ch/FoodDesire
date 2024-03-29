﻿namespace FoodDesire.Models;
public sealed class Address {
    public string? No { get; set; }
    public string? Street1 { get; set; } = string.Empty;
    public string? Street2 { get; set; }
    [Required]
    public string? City { get; set; }
    [Required]
    public int PostalCode { get; set; }
}
