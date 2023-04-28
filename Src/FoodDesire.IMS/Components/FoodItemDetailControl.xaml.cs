namespace FoodDesire.IMS.Components;
public sealed partial class FoodItemDetailControl : UserControl {
    public FoodItemDetail? ListDetailsMenuItem {
        get => GetValue(ListDetailsMenuItemProperty) as FoodItemDetail;
        set => SetValue(ListDetailsMenuItemProperty, value);
    }
    public static readonly DependencyProperty ListDetailsMenuItemProperty =
        DependencyProperty.Register("ListDetailsMenuItem", typeof(FoodItemDetail), typeof(FoodItemDetailControl), new PropertyMetadata(null));

    public FoodItemDetailControl() {
        InitializeComponent();
    }
}
