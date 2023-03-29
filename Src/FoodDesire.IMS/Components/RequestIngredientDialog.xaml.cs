namespace FoodDesire.IMS.Components;
public sealed partial class RequestIngredientDialog : ContentDialog {
    public RequestIngredientViewModel ViewModel { get; }

    public RequestIngredientDialog(int ingredientId) {
        InitializeComponent();
        ViewModel = new RequestIngredientViewModel(ingredientId);
    }
}
