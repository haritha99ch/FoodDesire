namespace FoodDesire.Models;
public sealed class Account : Entity {
    [Required]
    [EmailAddress(ErrorMessage = "Please enter a valid email")]
    public required string Email { get; set; }
    [Required]
    [MinLength(5)]
    [MaxLength(15, ErrorMessage = "Password must be 15 characters or less")]
    public required string Password { get; set; }
    public string? VerifyCode { get; set; }

    public User? User { get; set; }
}
