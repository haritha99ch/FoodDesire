namespace FoodDesire.IMS.Components;
public sealed partial class EditRecipeIngredientDialog : ContentDialog {
    public RecipeIngredientForm RecipeIngredient {
        get { return (RecipeIngredientForm)GetValue(RecipeIngredientProperty); }
        set { SetValue(RecipeIngredientProperty, value); }
    }
    public static readonly DependencyProperty RecipeIngredientProperty =
        DependencyProperty.Register("RecipeIngredient", typeof(RecipeIngredientForm), typeof(EditRecipeIngredientDialog), new PropertyMetadata(0));

    public EditRecipeIngredientDialog() {
        InitializeComponent();
    }

    private void ContentDialog_Loaded(object sender, RoutedEventArgs e) {
        RecipeIngredientFormControl.RecipeIngredient.SelectedIngredient =
            RecipeIngredientFormControl.ViewModel.RawIngredients.SingleOrDefault(e => e.Id == RecipeIngredient.Ingredient_Id);
        RecipeIngredientFormControl.RecipeIngredient.SelectedRecipeAsIngredient =
            RecipeIngredientFormControl.ViewModel.RecipeAsIngredients.SingleOrDefault(e => e.Id == RecipeIngredient.Recipe_Id);
    }
}
