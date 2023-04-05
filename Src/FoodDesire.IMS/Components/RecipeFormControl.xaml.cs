namespace FoodDesire.IMS.Components;
public sealed partial class RecipeFormControl : UserControl {

    public RecipeFormViewModel ViewModel { get; }
    public RecipeForm Recipe {
        get { return (RecipeForm)GetValue(RecipeProperty); }
        set { SetValue(RecipeProperty, value); }
    }
    public static readonly DependencyProperty RecipeProperty =
        DependencyProperty.Register("Recipe", typeof(RecipeForm), typeof(RecipeFormControl), new PropertyMetadata(0));

    public Grid ImagesContainer => imagesContainer;


    public RecipeFormControl() {
        ViewModel = App.GetService<RecipeFormViewModel>();
        InitializeComponent();
    }

    private void UserControl_Loaded(object sender, RoutedEventArgs e) {
        ViewModel.XamlRoot = XamlRoot;
        Recipe.XamlRoot = XamlRoot;
        //if (!ViewModel.RecipeCategories.Any(e => e.Id == Recipe.SelectedRecipeCategory!.Id))
        ViewModel.RecipeCategories.Add(Recipe.SelectedRecipeCategory!);
        Category.SelectedItem = Recipe.SelectedRecipeCategory;
    }

    private void HideFlyout(object sender, RoutedEventArgs e) {
        AddIngredientCategoryFlyout.Hide();
        EditIngredientCategoryFlyout.Hide();
    }
}
