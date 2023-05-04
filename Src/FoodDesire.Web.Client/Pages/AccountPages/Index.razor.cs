using Microsoft.AspNetCore.Components.Authorization;

namespace FoodDesire.Web.Client.Pages.AccountPages;
public partial class Index : ComponentBase {
    [Inject]
    private NavigationManager _navigationManager { get; set; } = default!;
    [Inject]
    private AuthenticationStateProvider _authenticationStateProvider { get; set; } = default!;
    [Inject]
    private IAccountPageService _accountPageService { get; set; } = default!;

    private bool _loading = true;
    private Customer? _customer = new();


    protected override async Task OnInitializedAsync() {
        _loading = true;
        AuthenticationState authState = await _authenticationStateProvider.GetAuthenticationStateAsync();
        if (!authState.User.Identity!.IsAuthenticated) {
            _navigationManager.NavigateTo("/Account/SignIn");
            return;
        }
        _customer = await _accountPageService!.Get();
        await base.OnInitializedAsync();
        _loading = false;
    }
}
