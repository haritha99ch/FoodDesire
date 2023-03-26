namespace FoodDesire.IMS.Components;
public sealed partial class EditIngredientDialog : ContentDialog {
    public EditIngredientViewModel ViewModel { get; }
    public EditIngredientDialog(int ingredientId) {
        ViewModel = new EditIngredientViewModel(ingredientId);
        InitializeComponent();
        FormControl.ViewModel = ViewModel;
        FormControl.NewCategoryFormControl!.ViewModel = ViewModel;
        FormControl.EditCategoryFormControl!.ViewModel = ViewModel;
    }
}
