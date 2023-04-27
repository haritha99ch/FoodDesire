namespace FoodDesire.Web.Client.Pages.OrderPages;
public partial class Index : ComponentBase {
    [Inject] private IOrderPageService _orderPageService { get; set; } = default!;
    [Inject] private IAuthenticationService _authenticationService { get; set; } = default!;
    [Inject] private NavigationManager _navigationManager { get; set; } = default!;

    private bool _loading = true;

    private List<Order> _orders = new();

    protected override async Task OnInitializedAsync() {
        await base.OnInitializedAsync();
        _loading = true;
        if (!await _authenticationService.IsAuthenticated()) {
            _navigationManager.NavigateTo("/Account/SignIn");
            return;
        }
        _orders = await _orderPageService.GetOrdersAsync() ?? new();
        _loading = false;
    }

    private void OrderDetails(int orderId) {
        _navigationManager.NavigateTo($"/Order/{orderId}");
    }

}
