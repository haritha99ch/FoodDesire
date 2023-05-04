namespace FoodDesire.Web.Client.Shared;
public partial class NavigationBarComponent : ComponentBase {
    [Inject] private NavigationManager _navigationManager { get; set; } = default!;
    [Inject] private IComponentCommunicationService<string> _searchCommunicationService { get; set; } = default!;
    [Inject] private IAuthenticationService _authenticationService { get; set; } = default!;
    [Inject] private IAccountPageService _accountPageService { get; set; } = default!;
    [Inject] private IComponentCommunicationService<Web.Shared.SignIn> _signInCommunicationService { get; set; } = default!;

    private bool _isAuthenticated = false;
    private string? Search { get; set; }
    private bool open = false;

    protected override async Task OnInitializedAsync() {
        await base.OnInitializedAsync();
        _signInCommunicationService.OnChange += UpdateAuthentication;
        _isAuthenticated = await _authenticationService.IsAuthenticated();
    }

    private void UpdateAuthentication(SignIn @in) {
        if (@in != null && !string.IsNullOrEmpty(@in.Email)) _isAuthenticated = true;
        StateHasChanged();
    }

    void ToggleDrawer() {
        open = !open;
    }

    private void NavigateTo(string pageUri) {
        open = false;
        _navigationManager.NavigateTo(pageUri);
    }

    private void OnSearchTextChanged() {
        if (!_navigationManager!.Uri.Contains("/Recipe/Index")) {
            NavigateTo("/Recipe/Index");
        }
        _searchCommunicationService.NotifyStateChanged(Search!);
    }

    private async void OnSignOutButtonClicked() {
        bool signOut = await _accountPageService.SignOut();
        if (signOut) NavigateTo("/");
        _isAuthenticated = await _authenticationService.IsAuthenticated();
        StateHasChanged();
    }
}
