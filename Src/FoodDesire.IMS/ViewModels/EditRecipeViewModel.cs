namespace FoodDesire.IMS.ViewModels;
public partial class EditRecipeViewModel : ObservableRecipient, INavigationAware {
    private readonly IRecipesPageService _recipesPageService;

    [ObservableProperty]
    private RecipeForm? _recipeForm;

    public EditRecipeViewModel(IRecipesPageService recipesPageService) {
        _recipesPageService = recipesPageService;
    }

    public async void OnNavigatedTo(object parameter) {
        if (parameter is Recipe recipe) {
            RecipeForm recipeForm = App.GetService<IMapper>().Map<RecipeForm>(recipe);
            recipeForm.SelectedRecipeCategory = recipe.RecipeCategory;
            RecipeForm = recipeForm;
        }
    }

    public void OnNavigatedFrom() { }
}
