namespace FoodDesire.IMS.Components;
public sealed partial class NewIngredientFormDialog : ContentDialog {
    public NewIngredientFormViewModel ViewModel { get; }
    public NewIngredientFormDialog(NewIngredientFormViewModel viewModel) {
        ViewModel = viewModel;
        InitializeComponent();
        FormControl.ViewModel = ViewModel;
        FormControl.NewCategoryFormControl!.ViewModel = ViewModel;
        FormControl.EditCategoryFormControl!.ViewModel = ViewModel;
    }
}
