namespace FoodDesire.IMS.Components;

public sealed partial class DeliveriesDetailControl : UserControl {
    public DeliveriesDetailViewModel ViewModel { get; }

    public Order? ListDetailsMenuItem {
        get => GetValue(ListDetailsMenuItemProperty) as Order;
        set => SetValue(ListDetailsMenuItemProperty, value);
    }

    public static readonly DependencyProperty ListDetailsMenuItemProperty =
        DependencyProperty.Register("ListDetailsMenuItem", typeof(Order), typeof(DeliveriesDetailControl), new PropertyMetadata(null, OnListDetailsMenuItemPropertyChanged));

    public DeliveriesDetailControl() {
        InitializeComponent();
        ViewModel = App.GetService<DeliveriesDetailViewModel>();
    }

    private static void OnListDetailsMenuItemPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) {
        if (d is DeliveriesDetailControl control) {
            control.ForegroundElement.ChangeView(0, 0, 1);
            control.ViewModel.OrderId = (e.NewValue as Order)?.Id ?? 0;
            _ = control.ViewModel.GetFoodItems();
        }
    }
}
