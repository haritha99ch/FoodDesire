namespace FoodDesire.Web.Client.Pages.Cart;
public partial class Index : ComponentBase {
    [Inject] private ICartPageService _cartPageService { get; set; } = default!;
    [Inject] private IAuthenticationService _authenticationService { get; set; } = default!;
    [Inject] private NavigationManager _navigationManager { get; set; } = default!;

    private Order _order = new();
    private List<FoodItem> _foodItems = new();

    protected override async void OnInitialized() {
        base.OnInitialized();
        if (!await _authenticationService.IsAuthenticated()) {
            _navigationManager.NavigateTo("/Account/SignIn");
            return;
        }
        _order = await _cartPageService.GetUserCurrentOrderAsync();
        _foodItems = await _cartPageService.GetFoodItemsForOrderAsync(_order.Id);
    }
}
