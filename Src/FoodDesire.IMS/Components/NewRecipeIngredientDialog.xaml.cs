namespace FoodDesire.IMS.Components;
public sealed partial class NewRecipeIngredientDialog : ContentDialog {
    public RecipeIngredientForm RecipeIngredient {
        get { return (RecipeIngredientForm)GetValue(RecipeIngredientProperty); }
        set { SetValue(RecipeIngredientProperty, value); }
    }
    public static readonly DependencyProperty RecipeIngredientProperty =
        DependencyProperty.Register("RecipeIngredient", typeof(RecipeIngredientForm), typeof(NewRecipeIngredientDialog), new PropertyMetadata(0));


    public NewRecipeIngredientDialog() {
        RecipeIngredient = new();
        InitializeComponent();
    }
}
