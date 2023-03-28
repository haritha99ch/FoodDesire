using CommunityToolkit.Mvvm.Input;
using System.Diagnostics;

namespace FoodDesire.IMS.ViewModels;

public partial class SuppliesViewModel : ObservableRecipient, INavigationAware {
    private readonly ISuppliesPageService _suppliesPageService;
    private readonly IUserService<Supplier> _supplierUserService;
    private Supplier? _supplier;
    public ObservableCollection<Supply> PendingSupplies { get; set; } = new();
    public ObservableCollection<Supply> UserSupplies { get; set; } = new();
    public SuppliesViewModel(ISuppliesPageService suppliesPageService, IUserService<Supplier> supplierUserService) {
        _suppliesPageService = suppliesPageService;
        _supplierUserService = supplierUserService;
    }
    [ObservableProperty]
    private bool _isAcceptingSupply = false;

    public async void OnNavigatedTo(object parameter) {
        List<Supply> pendingSupplies = await _suppliesPageService.GetAllPendingSupplies();
        pendingSupplies.ForEach(PendingSupplies.Add);

        if (App.CurrentUserAccount!.Role != Role.Supplier) return;
        _supplier = await _supplierUserService.GetByEmail(App.CurrentUserAccount!.Email!);
        List<Supply> userSuppliesSupplies = await _suppliesPageService.GetAcceptedSuppliesForSupplier(_supplier.Id);
        userSuppliesSupplies.ForEach(UserSupplies.Add);
    }

    public void OnNavigatedFrom() { }

    [RelayCommand]
    public async void AcceptSupply(int supplyId) {
        IsAcceptingSupply = true;
        Supply acceptedSupply = await _suppliesPageService.AcceptSupply(supplyId, _supplier!.Id);
        UserSupplies.Add(acceptedSupply);
        PendingSupplies.Remove(PendingSupplies.FirstOrDefault(s => s.Id == supplyId)!);
        IsAcceptingSupply = false;
    }

    [RelayCommand]
    public void SupplyItemClicked(object id) {
        Debug.WriteLine("Hello mf");
    }
}
