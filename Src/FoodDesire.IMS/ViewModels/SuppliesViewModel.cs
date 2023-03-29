using CommunityToolkit.Mvvm.Input;

namespace FoodDesire.IMS.ViewModels;
public partial class SuppliesViewModel : ObservableRecipient, INavigationAware {
    private readonly ISuppliesPageService _suppliesPageService;
    private readonly IUserService<Supplier> _supplierUserService;

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(IsSupplier), nameof(PendingSuppliesGridIndex))]
    private Supplier? _supplier;
    public ObservableCollection<Supply> PendingSupplies { get; set; } = new();
    public ObservableCollection<Supply> UserSupplies { get; set; } = new();

    public bool IsSupplier => Supplier != null;
    public int PendingSuppliesGridIndex => (Supplier == null) ? 0 : 1;

    public SuppliesViewModel(ISuppliesPageService suppliesPageService, IUserService<Supplier> supplierUserService) {
        _suppliesPageService = suppliesPageService;
        _supplierUserService = supplierUserService;
    }

    public async void OnNavigatedTo(object parameter) {
        List<Supply> pendingSupplies = await _suppliesPageService.GetAllPendingSupplies();
        pendingSupplies.ForEach(PendingSupplies.Add);

        if (App.CurrentUserAccount!.Role != Role.Supplier) return;
        Supplier = await _supplierUserService.GetByEmail(App.CurrentUserAccount!.Email!);
        List<Supply> userSuppliesSupplies = await _suppliesPageService.GetAcceptedSuppliesForSupplier(Supplier.Id);
        userSuppliesSupplies.ForEach(UserSupplies.Add);
    }

    public void OnNavigatedFrom() { }

    [RelayCommand]
    public async void AcceptSupply(int supplyId) {
        Supply acceptedSupply = await _suppliesPageService.AcceptSupply(supplyId, Supplier!.Id);
        UserSupplies.Add(acceptedSupply);
        PendingSupplies.Remove(PendingSupplies.FirstOrDefault(s => s.Id == supplyId)!);
    }
}
