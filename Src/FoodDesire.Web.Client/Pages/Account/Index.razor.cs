namespace FoodDesire.Web.Client.Pages.Account;
public partial class Index : ComponentBase {
    [Inject]
    private NavigationManager? _navigationManager { get; set; }
    [Inject]
    private IAuthenticationService? _authenticationService { get; set; }
    [Inject]
    private IAccountPageService? _accountPageService { get; set; }

    private Customer? customer = new();


    protected override async Task OnInitializedAsync() {
        if (!await _authenticationService!.IsAuthenticated()) {
            _navigationManager!.NavigateTo("/Account/SignUp");
            return;
        }
        customer = await _accountPageService!.Get();
        await base.OnInitializedAsync();
    }
}
