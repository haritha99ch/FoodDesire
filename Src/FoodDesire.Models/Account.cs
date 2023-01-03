namespace FoodDesire.Models;
public sealed class Account: Entity {
    [Required, NotNull]
    [EmailAddress(ErrorMessage = "Please enter a valid email")]
    public string? Email { get; set; }
    [Required, NotNull]
    [MinLength(5)]
    [MaxLength(15, ErrorMessage = "Password must be 15 characters or less")]
    public string? Password { get; set; }
    public string? VerifyCode { get; set; }

    public User? User { get; set; }
}
