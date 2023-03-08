namespace FoodDesire.IMS.ViewModels;
public class HomeViewModel : ObservableRecipient, IInitializable {
    private readonly IHomeService _homeService;
    private InventorySummary _inventorySummary = new();
    private bool _isLoading;

    public InventorySummary InventorySummary {
        get => _inventorySummary;
        set => SetProperty(ref _inventorySummary, value);
    }
    public bool IsLoading { get => _isLoading; set => SetProperty(ref _isLoading, value); }

    public HomeViewModel(IHomeService homeService) {
        _homeService = homeService;
        _ = OnInit();
    }

    public async Task OnInit() {
        InventorySummary = await _homeService.GetInventorySummery();
        IsLoading = false;
    }
}
