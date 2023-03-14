namespace FoodDesire.IMS.Components;
public sealed partial class EditIngredientDialog : ContentDialog {
    public EditIngredientViewModel ViewModel { get; }
    public EditIngredientDialog(Ingredient ingredient) {
        ViewModel = new EditIngredientViewModel(ingredient);
        InitializeComponent();
        FormControl.DataContext = ViewModel;
    }
}
