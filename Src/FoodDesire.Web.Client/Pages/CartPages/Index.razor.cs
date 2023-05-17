using Microsoft.JSInterop;

namespace FoodDesire.Web.Client.Pages.CartPages;
public partial class Index : ComponentBase {
    [Inject] private ICartPageService _cartPageService { get; set; } = default!;
    [Inject] private IAuthenticationService _authenticationService { get; set; } = default!;
    [Inject] private NavigationManager _navigationManager { get; set; } = default!;
    [Inject] private IDialogService _dialogService { get; set; } = default!;
    [Inject] private IJSRuntime _jSRuntime { get; set; } = default!;
    [Inject] private IMapper _mapper { get; set; } = default!;
    [Inject] private ISnackbar _snackBar { get; set; } = default!;

    private bool _loading = true;
    private bool _presentingPayPalOrder = false;
    private bool _showPayPalButtons = false;
    private bool _paymentCompleted = false;

    private Order? _order;
    private List<FoodItemListItem> _foodItems = new();

    private DotNetObjectReference<Index> _objRef { get; set; } = default!;

    protected override async Task OnInitializedAsync() {
        await base.OnInitializedAsync();
        _objRef = DotNetObjectReference.Create(this);
        _loading = true;
        if (!await _authenticationService.IsAuthenticated()) {
            _navigationManager.NavigateTo("/Account/SignIn");
            return;
        }
        await LoadFoodItems();
        _loading = false;
    }

    private async Task OnItemDelete(int foodItemId) {
        var options = new DialogOptions { CloseOnEscapeKey = true, MaxWidth = MaxWidth.Medium };
        var result = await _dialogService.Show<DeleteConfirmationDialogComponent>("Delete From Cart", options).Result;

        if (result.Canceled) return;

        bool deleted = await _cartPageService.RemoveFoodItemByIdAsync(foodItemId);
        if (!deleted) return;
        await LoadFoodItems();
    }

    private async Task LoadFoodItems() {
        _loading = true;
        await InvokeAsync(StateHasChanged);
        _order = await _cartPageService.GetUserCurrentOrderAsync();
        if (_order == null) {
            _loading = false;
            await InvokeAsync(StateHasChanged);
            return;
        }
        _foodItems.Clear();
        _foodItems = await _cartPageService.GetFoodItemsForOrderAsync(_order.Id);
        _loading = false;
        await InvokeAsync(StateHasChanged);
    }

    private async Task DeleteOrder() {
        var options = new DialogOptions { CloseOnEscapeKey = true, MaxWidth = MaxWidth.Medium };
        var result = await _dialogService.Show<DeleteConfirmationDialogComponent>("Delete The Order", options).Result;

        if (result.Canceled) return;

        bool orderDeleted = await _cartPageService.CancelOrderAsync(_order!.Id);
        if (!orderDeleted) return;
        _navigationManager.NavigateTo("/Recipe/Index");
    }

    private async Task PayForOrder() {
        _presentingPayPalOrder = true;
        _showPayPalButtons = true;

        string? orderId = await _cartPageService.PayForOrderAsync(_order!.Id);

        await _jSRuntime.InvokeVoidAsync("presentPayPalOrder", orderId, _objRef);

        _presentingPayPalOrder = false;
        _showPayPalButtons = true;
    }

    [JSInvokable]
    public async Task HandleShippingAddress(ShippingAddress shippingAddress) {
        _presentingPayPalOrder = true;
        // Handle the shipping address here
        _order!.Delivery = new() {
            Address = _mapper.Map<Address>(shippingAddress),
            IsDelivered = false
        };
        Order order = await _cartPageService.CompletePayment(_order);
        if (order.Status.Equals(OrderStatus.Ordered)) {
            _snackBar.Configuration.PositionClass = Defaults.Classes.Position.BottomCenter;
            _snackBar.Add("Payment completed successfully!", Severity.Success);
            _navigationManager.NavigateTo("/Order/Index");
        }
        _presentingPayPalOrder = false;
    }
}
