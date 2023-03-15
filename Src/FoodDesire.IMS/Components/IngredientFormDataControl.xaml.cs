namespace FoodDesire.IMS.Components;
public sealed partial class IngredientFormDataControl : UserControl {
    public IngredientForm? ViewModel { get; set; }
    public IngredientFormDataControl() {
        InitializeComponent();
    }

    private void Button_Click(object sender, RoutedEventArgs e) {
        AddIngredientCategoryFlyout.Hide();
    }
}
