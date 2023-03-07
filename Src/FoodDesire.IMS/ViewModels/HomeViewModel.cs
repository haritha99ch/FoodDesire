namespace FoodDesire.IMS.ViewModels;
public class HomeViewModel : ObservableRecipient {
    private readonly IHomeService _homeService;

    private InventorySummary _inventorySummary = new();
    public InventorySummary InventorySummary {
        get => _inventorySummary;
        set => SetProperty(ref _inventorySummary, value);
    }

    public HomeViewModel(IHomeService homeService) {
        _homeService = homeService;
        _ = OnInit();
    }

    public async Task OnInit() {
        InventorySummary = await _homeService.GetInventorySummery();
    }
}
