namespace FoodDesire.IMS.Controls;
public sealed partial class ViewIngredientItemControl : UserControl {
    public ViewIngredientItemViewModel ViewModel { get; }

    public ViewIngredientItemControl(IngredientDetails ingredientDetails) {
        ViewModel = new ViewIngredientItemViewModel(ingredientDetails);
        DataContext = ViewModel;
        InitializeComponent();
    }
}
