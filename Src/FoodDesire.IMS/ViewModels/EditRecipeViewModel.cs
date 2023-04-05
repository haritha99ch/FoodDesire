namespace FoodDesire.IMS.ViewModels;
public partial class EditRecipeViewModel : ObservableRecipient, INavigationAware {
    private readonly IRecipesPageService _recipesPageService;

    [ObservableProperty]
    private RecipeForm? _recipeForm;

    public EditRecipeViewModel(IRecipesPageService recipesPageService) {
        _recipesPageService = recipesPageService;
    }

    public async void OnNavigatedTo(object parameter) {
        if (parameter is int recipeId) {
            Recipe recipe = await _recipesPageService.GetRecipeById(recipeId);
            RecipeForm recipeForm = App.GetService<IMapper>().Map<RecipeForm>(recipe);
            recipeForm.SelectedRecipeCategory = await _recipesPageService.GetRecipeCategoryById(recipe.RecipeCategoryId);
            RecipeForm = recipeForm;
        }
    }

    public void OnNavigatedFrom() { }
}
