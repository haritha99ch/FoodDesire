using Microsoft.AspNetCore.Components.Forms;

namespace FoodDesire.Web.Client.Pages.CartPages;
public partial class Index : ComponentBase {
    [Inject] private ICartPageService _cartPageService { get; set; } = default!;
    [Inject] private IAuthenticationService _authenticationService { get; set; } = default!;
    [Inject] private NavigationManager _navigationManager { get; set; } = default!;
    [Inject] private IDialogService _dialogService { get; set; } = default!;

    private bool _loading = true;

    private Order? _order;
    private Address _address = new();
    private List<FoodItemListItem> _foodItems = new();

    protected override async Task OnInitializedAsync() {
        await base.OnInitializedAsync();
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

        bool orderDeleted = await _cartPageService.CancelOrderAsync(_order.Id);
        if (!orderDeleted) return;
        _navigationManager.NavigateTo("/Recipe/Index");
    }

    private async Task PayForOrder(EditContext context) {
        if (!context.Validate()) return;
        //TODO: Implement Payment Getaway
        _order!.Delivery = new() {
            Address = _address,
            IsDelivered = false,
        };
        Order order = await _cartPageService.UpdateOrderAsync(_order);
        if (order == null) return;
        _order = order;
        _order.Delivery = null;
        _order = await _cartPageService.PayForOrderAsync(_order.Id);
        _navigationManager.NavigateTo("/Recipe/Index");
    }
}
