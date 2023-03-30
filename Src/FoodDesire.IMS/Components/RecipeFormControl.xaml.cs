namespace FoodDesire.IMS.Components;
public sealed partial class RecipeFormControl : UserControl {

    public RecipeFormViewModel ViewModel { get; }
    public RecipeForm Recipe {
        get { return (RecipeForm)GetValue(RecipeProperty); }
        set { SetValue(RecipeProperty, value); }
    }
    public static readonly DependencyProperty RecipeProperty =
        DependencyProperty.Register("Recipe", typeof(RecipeForm), typeof(RecipeFormControl), new PropertyMetadata(0));

    public RecipeFormControl() {
        ViewModel = App.GetService<RecipeFormViewModel>();
        InitializeComponent();
    }

    private void UserControl_Loaded(object sender, RoutedEventArgs e) {
        Recipe.RecipeCategories = ViewModel.RecipeCategories;
    }

    private void HideFlyout(object sender, RoutedEventArgs e) {
        AddIngredientCategoryFlyout.Hide();
        EditIngredientCategoryFlyout.Hide();
    }
}
