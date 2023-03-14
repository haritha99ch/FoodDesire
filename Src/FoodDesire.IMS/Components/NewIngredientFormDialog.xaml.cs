namespace FoodDesire.IMS.Components;
public sealed partial class NewIngredientFormDialog : ContentDialog {
    public NewIngredientFormViewModel ViewModel { get; }
    public NewIngredientFormDialog() {
        ViewModel = new NewIngredientFormViewModel(App.GetService<IIngredientsPageService>());
        InitializeComponent();
        FormControl.ViewModel = ViewModel;
    }
}
