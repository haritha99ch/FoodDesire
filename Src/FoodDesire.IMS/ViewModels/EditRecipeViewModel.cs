namespace FoodDesire.IMS.ViewModels;
public partial class EditRecipeViewModel : ObservableRecipient, INavigationAware {
    [ObservableProperty]
    private RecipeForm? _recipeForm;

    public EditRecipeViewModel() { }

    public void OnNavigatedTo(object parameter) {
        if (parameter is not Recipe recipe) return;
        RecipeForm recipeForm = App.GetService<IMapper>().Map<RecipeForm>(recipe);
        recipeForm.SelectedRecipeCategory = recipe.RecipeCategory;
        RecipeForm = recipeForm;
    }

    public void OnNavigatedFrom() { }
}
