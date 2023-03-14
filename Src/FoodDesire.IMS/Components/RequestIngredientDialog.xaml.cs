namespace FoodDesire.IMS.Components;
public sealed partial class RequestIngredientDialog : ContentDialog {
    public RequestIngredientViewModel ViewModel { get; }

    public RequestIngredientDialog(IngredientDetails ingredientDetails) {
        ViewModel = new RequestIngredientViewModel(ingredientDetails);
        InitializeComponent();
    }
}
