namespace FoodDesire.IMS.Components;
public sealed partial class RecipeIngredientFormControl : UserControl {
    public RecipeIngredientForm RecipeIngredient {
        get { return (RecipeIngredientForm)GetValue(RecipeIngredientProperty); }
        set { SetValue(RecipeIngredientProperty, value); }
    }
    public static readonly DependencyProperty RecipeIngredientProperty =
        DependencyProperty.Register("RecipeIngredient", typeof(RecipeIngredientForm), typeof(RecipeIngredientFormControl), new PropertyMetadata(0));
    public RecipeIngredientFormViewModel ViewModel { get; }

    public RecipeIngredientFormControl() {
        ViewModel = App.GetService<RecipeIngredientFormViewModel>();
        InitializeComponent();
    }

    private void Recipe_SelectionChanged(object sender, SelectionChangedEventArgs e) {
        RawIngredient.SelectedItem = null;
    }

    private void RawIngredient_SelectionChanged(object sender, SelectionChangedEventArgs e) {
        Recipe.SelectedItem = null;
    }
}
