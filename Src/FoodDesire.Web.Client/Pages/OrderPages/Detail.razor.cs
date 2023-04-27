namespace FoodDesire.Web.Client.Pages.OrderPages;
public partial class Detail : ComponentBase {
    [Inject] private IOrderPageService _orderPageService { get; set; } = default!;
    [Inject] private ICartPageService _cartPageService { get; set; } = default!;

    [Parameter] public int OrderId { get; set; }

    private bool _loading = true;

    private Order? _order;
    private List<FoodItemListItem> _foodItems = new();

    protected override async Task OnInitializedAsync() {
        await base.OnInitializedAsync();
        _loading = true;
        _order = await _orderPageService.GetAsync(OrderId);
        if (_order != null) await GetFoodItems();
        _loading = false;
    }

    private async Task GetFoodItems() => _foodItems = await _cartPageService.GetFoodItemsForOrderAsync(OrderId);
}
