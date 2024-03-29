﻿namespace FoodDesire.Models;
public sealed class Account : Entity {
    [Required]
    [EmailAddress(ErrorMessage = "Please enter a valid email")]
    public string? Email { get; set; }
    public string? Password { get; set; }
    public string? VerifyCode { get; set; }
    [Required]
    public Role Role { get; set; }
    public byte[]? ProfilePicture { get; set; }
}

public enum Role {
    [Display(Name = "Admin")]
    Admin,
    [Display(Name = "Chef")]
    Chef,
    [Display(Name = "Supplier")]
    Supplier,
    [Display(Name = "Deliverer")]
    Deliverer,
    [Display(Name = "Customer")]
    Customer
}
