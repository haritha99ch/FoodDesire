namespace FoodDesire.IMS.ViewModels;
public partial class DeliveriesViewModel : ObservableRecipient, INavigationAware {
    private readonly IDeliveriesPageService _deliveriesPageService;
    private readonly IUserService<Deliverer> _delivererUserService;

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(IsDeliverer), nameof(PreparedOrdersGridIndex))]
    private Deliverer? _deliverer;
    public ObservableCollection<Order> PreparedOrders { get; set; } = new();
    public bool IsDeliverer => Deliverer != null;
    public int PreparedOrdersGridIndex => (Deliverer == null) ? 0 : 1;

    public ObservableCollection<FoodItem> FoodItems { get; set; } = new();

    private Order? _selected;
    public Order? Selected {
        get => _selected;
        set {
            SetProperty(ref _selected, value);
            OnPropertyChanged(nameof(ItemSelected));
        }
    }
    public bool ItemSelected => Selected != null;
    [ObservableProperty]
    private bool _delivererOrdersOnly = false;


    public DeliveriesViewModel(IDeliveriesPageService deliveriesPageService, IUserService<Deliverer> delivererUserService) {
        _deliveriesPageService = deliveriesPageService;
        _delivererUserService = delivererUserService;
    }

    public async void OnNavigatedTo(object parameter) {
        if (App.CurrentUserAccount!.Role == Role.Deliverer)
            Deliverer = await _delivererUserService.GetByEmail(App.CurrentUserAccount!.Email!);
        await UpdateOrders();
    }

    public void OnNavigatedFrom() { }

    public void EnsureItemSelected() {
        Selected = PreparedOrders.FirstOrDefault();
    }

    [RelayCommand]
    private async Task UpdateOrders() {
        PreparedOrders.Clear();
        if (DelivererOrdersOnly) {
            List<Order> userDeliveryOrders = await _deliveriesPageService.GetMyOrdersToDeliver(Deliverer!.Id);
            userDeliveryOrders.ForEach(PreparedOrders.Add);
            return;
        }
        List<Order> preparedOrders = await _deliveriesPageService.GetOrdersToDeliver();
        preparedOrders.ForEach(PreparedOrders.Add);
    }

    [RelayCommand]
    private async Task AcceptDeliveryForOrder() {
        Order acceptedOrder = await _deliveriesPageService.TakeOrderToDeliveryList(Selected!.Id, Deliverer!.Id);
        if (acceptedOrder.Status.Equals(OrderStatus.Delivering)) await UpdateOrders();
    }

    [RelayCommand]
    private async Task CompleteOrder() {
        Order order = await _deliveriesPageService.CompleteDeliveryForOrder(Selected!.Id);
        if (order.Status.Equals(OrderStatus.Delivered)) await UpdateOrders();
    }
}
