using MudBlazor;
using System.Text.RegularExpressions;

namespace FoodDesire.Web.Client.Pages.Account;
public partial class SignUp {
    [Inject]
    private NavigationManager _navigationManager { get; set; } = default!;
    [Inject]
    private IAccountPageService _accountPageService { get; set; } = default!;
    [Inject]
    private IComponentCommunicationService<Web.Shared.SignIn> _signInCommunicationService { get; set; } = default!;

    private User user = new() {
        Account = new() {
            Role = Role.Customer
        },
        Address = new(),
        PhoneNumber = string.Empty,
    };
    private MudForm? form;
    private MudTextField<string>? passwordField1;

    private IEnumerable<string> PasswordStrength(string password) {
        if (string.IsNullOrWhiteSpace(password)) {
            yield return "Password is required!";
            yield break;
        }
        if (password.Length < 8)
            yield return "Password must be at least of length 8";
        if (!Regex.IsMatch(password, @"[A-Z]"))
            yield return "Password must contain at least one capital letter";
        if (!Regex.IsMatch(password, @"[a-z]"))
            yield return "Password must contain at least one lowercase letter";
        if (!Regex.IsMatch(password, @"[0-9]"))
            yield return "Password must contain at least one digit";
    }

    private string PasswordMatch(string arg) {
        if (passwordField1?.Value != arg)
            return "Passwords don't match";
        return null!;
    }

    private async void Register() {
        await form!.Validate();
        if (!form.IsValid) return;
        Customer? customer = await _accountPageService.SignUp(user);
        _signInCommunicationService.Value = new() {
            Email = customer!.User!.Account!.Email!,
            Password = customer.User.Account.Password!,
        };
        _navigationManager.NavigateTo("/Account/SignIn");
    }
}
