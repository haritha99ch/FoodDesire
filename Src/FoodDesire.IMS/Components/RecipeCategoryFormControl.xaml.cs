namespace FoodDesire.IMS.Components;
public sealed partial class RecipeCategoryFormControl : UserControl {
    public RecipeForm Recipe {
        get { return (RecipeForm)GetValue(RecipeProperty); }
        set { SetValue(RecipeProperty, value); }
    }
    public static readonly DependencyProperty RecipeProperty =
        DependencyProperty.Register("Recipe", typeof(RecipeForm), typeof(RecipeCategoryFormControl), new PropertyMetadata(0));
    public RecipeCategoryFormControl() {
        InitializeComponent();
    }
}
