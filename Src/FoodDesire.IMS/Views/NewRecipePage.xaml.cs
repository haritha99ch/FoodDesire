namespace FoodDesire.IMS.Views;
public sealed partial class NewRecipePage : Page {
    public NewRecipeViewModel ViewModel { get; }
    public NewRecipePage() {
        ViewModel = App.GetService<NewRecipeViewModel>();
        InitializeComponent();
        RecipeForm.Recipe = new();
    }
}
