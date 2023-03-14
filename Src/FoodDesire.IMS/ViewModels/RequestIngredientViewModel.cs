namespace FoodDesire.IMS.ViewModels;
public partial class RequestIngredientViewModel : ObservableObject {
    [ObservableProperty]
    private IngredientDetails _ingredientDetails;
    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(CanRequest))]
    [NotifyPropertyChangedFor(nameof(RequestingAmount))]
    private double currentAmount;
    public double RequestingAmount => CurrentAmount - IngredientDetails.CurrentQuantity;
    public bool CanRequest => CurrentAmount != IngredientDetails.CurrentQuantity;

    public RequestIngredientViewModel(IngredientDetails ingredientDetails) {
        _ingredientDetails = new();
        IngredientDetails = ingredientDetails;
        currentAmount = IngredientDetails.CurrentQuantity;
    }
}
