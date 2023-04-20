using System.ComponentModel.DataAnnotations;

namespace FoodDesire.Web.Shared;
public class SignIn {
    [EmailAddress]
    [Required(ErrorMessage = "Email address is required!")]
    public string Email { get; set; } = string.Empty;
    [Required]
    [MinLength(8, ErrorMessage = "Password must be at least of length 8")]
    public string Password { get; set; } = string.Empty;
}
