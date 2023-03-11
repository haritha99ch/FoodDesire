namespace FoodDesire.IMS.ViewModels;
public partial class ViewIngredientItemViewModel : ObservableObject {
    [ObservableProperty]
    private IngredientDetails ingredientDetails;
    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(IsRequested))]
    [NotifyPropertyChangedFor(nameof(RequestingAmount))]
    private double currentAmount;
    public double RequestingAmount => CurrentAmount - IngredientDetails.CurrentQuantity;
    public bool IsRequested => CurrentAmount != IngredientDetails.CurrentQuantity;

    public ViewIngredientItemViewModel(IngredientDetails ingredientDetails) {
        IngredientDetails = ingredientDetails;
        currentAmount = IngredientDetails.CurrentQuantity;
    }
}
