namespace FoodDesire.IMS.ViewModels;
public partial class DeliveriesViewModel : ObservableRecipient, INavigationAware {
    private readonly IDeliveriesPageService _deliveriesPageService;
    private readonly IUserService<Deliverer> _delivererUserService;

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(IsDeliverer), nameof(PreparedOrdersGridIndex))]
    private Deliverer? _deliverer;
    public ObservableCollection<Order> PreparedOrders { get; set; } = new();
    public ObservableCollection<Order> UserOrders { get; set; } = new();
    public bool IsDeliverer => Deliverer != null;
    public int PreparedOrdersGridIndex => (Deliverer == null) ? 0 : 1;

    private Order? _selected;
    public Order? Selected {
        get => _selected;
        set {
            SetProperty(ref _selected, value);
            OnPropertyChanged(nameof(ItemSelected));
        }
    }
    public bool ItemSelected => Selected != null;


    public DeliveriesViewModel(IDeliveriesPageService deliveriesPageService, IUserService<Deliverer> delivererUserService) {
        _deliveriesPageService = deliveriesPageService;
        _delivererUserService = delivererUserService;
    }
    public async void OnNavigatedTo(object parameter) {
        List<Order> preparedOrders = await _deliveriesPageService.GetOrdersToDeliver();
        preparedOrders.ForEach(PreparedOrders.Add);

        if (App.CurrentUserAccount!.Role != Role.Deliverer) return;
        Deliverer = await _delivererUserService.GetByEmail(App.CurrentUserAccount!.Email!);
        List<Order> userDeliveryOrders = await _deliveriesPageService.GetMyOrdersToDeliver(Deliverer.Id);
        userDeliveryOrders.ForEach(UserOrders.Add);
    }

    public void OnNavigatedFrom() { }

    public void EnsureItemSelected() {
        Selected = PreparedOrders.FirstOrDefault();
    }

    [RelayCommand]
    private async Task AcceptDeliveryForOrder(int orderId) {
        Order acceptedOrder = await _deliveriesPageService.TakeOrderToDeliveryList(orderId, Deliverer!.Id);
        UserOrders.Add(acceptedOrder);
        PreparedOrders.Remove(PreparedOrders.FirstOrDefault(e => e.Id == orderId)!);
    }

    [RelayCommand]
    private async Task CompleteOrder(int orderId) {
        Order order = await _deliveriesPageService.CompleteDeliveryForOrder(orderId);
        if (order.Status.Equals(OrderStatus.Delivered)) UserOrders.Remove(UserOrders.FirstOrDefault(e => e.Id == orderId)!);
    }
}
