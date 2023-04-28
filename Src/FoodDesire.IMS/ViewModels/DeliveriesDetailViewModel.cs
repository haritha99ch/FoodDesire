namespace FoodDesire.IMS.ViewModels;
public partial class DeliveriesDetailViewModel : ObservableRecipient {
    private readonly IFoodItemService _foodItemService;

    private int _orderId;
    public int OrderId {
        get => _orderId;
        set {
            SetProperty(ref _orderId, value);
            OnPropertyChanged(nameof(GetFoodItems));
        }
    }

    public ObservableCollection<FoodItem> FoodItems { get; set; } = new();

    public DeliveriesDetailViewModel(IFoodItemService foodItemService) {
        _foodItemService = foodItemService;
    }

    public async Task GetFoodItems() {
        FoodItems.Clear();
        List<FoodItem>? foodItems = await _foodItemService.GetAllFoodItemsForOrder(OrderId);
        foodItems.ForEach(FoodItems.Add);
    }
}
