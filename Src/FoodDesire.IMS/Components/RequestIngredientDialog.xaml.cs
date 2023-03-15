namespace FoodDesire.IMS.Components;
public sealed partial class RequestIngredientDialog : ContentDialog {
    public RequestIngredientViewModel ViewModel { get; }

    public RequestIngredientDialog(int ingredientId) {
        ViewModel = new RequestIngredientViewModel(ingredientId);
        InitializeComponent();
    }
}
