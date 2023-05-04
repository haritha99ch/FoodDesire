using Microsoft.AspNetCore.Components.Forms;

namespace FoodDesire.Web.Client.Pages.AccountPages;
public partial class SignIn : ComponentBase {
    [Inject]
    private NavigationManager _navigationManager { get; set; } = default!;
    [Inject]
    private IAccountPageService _accountPageService { get; set; } = default!;
    [Inject]
    private IComponentCommunicationService<Web.Shared.SignIn> _signInCommunicationService { get; set; } = default!;

    private Web.Shared.SignIn _signIn { get; set; } = new() {
        Email = string.Empty,
        Password = string.Empty,
    };
    private MudForm _form { get; set; } = default!;
    private bool _invalidSignIn { get; set; } = false;

    protected override async Task OnInitializedAsync() {
        await base.OnInitializedAsync();
        if (_signInCommunicationService.Value == null) return;
        _signIn = _signInCommunicationService.Value;
        string? token = await _accountPageService.SignIn(_signIn);
        if (token == null) return;
        NavigateToIndex();
    }

    private void NavigateToIndex() => _navigationManager.NavigateTo("/Account/Index");

    private void NavigateToSignUp() => _navigationManager.NavigateTo("/Account/SignUp");

    private async Task SignInAsync(EditContext context) {
        StateHasChanged();
        string? token = await _accountPageService.SignIn(_signIn);
        if (token != null) {
            OnSignIn();
            NavigateToIndex();
        }
        _invalidSignIn = true;
    }

    private void OnSignIn() {
        _signInCommunicationService.NotifyStateChanged(_signIn);
    }
}
