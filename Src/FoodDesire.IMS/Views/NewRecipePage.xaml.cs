namespace FoodDesire.IMS.Views;
public sealed partial class NewRecipePage : Page {
    public NewRecipePage() {
        InitializeComponent();
        RecipeForm.Recipe = new();
    }
}
