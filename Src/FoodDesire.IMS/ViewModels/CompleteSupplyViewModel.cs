using CommunityToolkit.Mvvm.Input;

namespace FoodDesire.IMS.ViewModels;
public partial class CompleteSupplyViewModel : ObservableObject {
    private readonly ISuppliesPageService _suppliesPageService;

    [ObservableProperty]
    private Supply? _supply;
    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(IsPrimaryButtonEnabled))]
    private float _value = 0;

    public bool IsPrimaryButtonEnabled => Value != null;
    public SupplyResult Result { get; set; } = SupplyResult.Failed;
    public bool IsLoading => CompleteSupplyCommand.IsRunning;

    public CompleteSupplyViewModel(ISuppliesPageService suppliesPageService) {
        _suppliesPageService = suppliesPageService;
    }

    [RelayCommand]
    private async Task CompleteSupply(ContentDialog sender) {
        Supply = await _suppliesPageService.CompleteSupply(Supply.Id, (decimal)Value);
        Result = (Supply.Status.Equals(SupplyStatus.Completed)) ? SupplyResult.Completed : SupplyResult.Failed;
        sender.Hide();
    }
}

public enum SupplyResult {
    Completed,
    Failed
}

