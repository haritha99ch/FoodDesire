namespace FoodDesire.IMS.Components;
public sealed partial class EmployeesDetailControl : UserControl {
    public UserDetail? ListDetailsMenuItem {
        get => GetValue(ListDetailsMenuItemProperty) as UserDetail;
        set => SetValue(ListDetailsMenuItemProperty, value);
    }

    public static readonly DependencyProperty ListDetailsMenuItemProperty =
        DependencyProperty.Register("ListDetailsMenuItem", typeof(UserDetail), typeof(EmployeesDetailControl), new PropertyMetadata(null, OnListDetailsMenuItemPropertyChanged));

    public EmployeesDetailControl() {
        InitializeComponent();
    }

    private static void OnListDetailsMenuItemPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) {
        if (d is EmployeesDetailControl control) {
            control.ForegroundElement.ChangeView(0, 0, 1);
        }
    }
}
