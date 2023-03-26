namespace FoodDesire.IMS.Components;
public sealed partial class IngredientFormDataControl : UserControl {
    public IngredientForm? ViewModel { get; set; }
    public IngredientCategoryFormControl? NewCategoryFormControl => NewIngredientCategoryFormControl;
    public IngredientCategoryFormControl? EditCategoryFormControl => EditIngredientCategoryFormControl;
    public IngredientFormDataControl() {
        InitializeComponent();
    }

    private void Button_Click(object sender, RoutedEventArgs e) {
        AddIngredientCategoryFlyout.Hide();
        EditIngredientCategoryFlyout.Hide();
        DeleteIngredientCategoryFlyout.Hide();
    }

    private void EditCategoryButton_Click(object sender, RoutedEventArgs e) {
        ViewModel!.SetIngredientCategoryToEdit();
    }

    private void NewCategoryButton_Click(object sender, RoutedEventArgs e) {
        ViewModel!.NewIngredientCategoryName = string.Empty;
        ViewModel!.NewIngredientCategoryDescription = string.Empty;
    }
}
