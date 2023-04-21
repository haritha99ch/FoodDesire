using CommunityToolkit.Mvvm.Input;

namespace FoodDesire.IMS.ViewModels;
public partial class RecipeFormViewModel : ObservableRecipient {
    private readonly IRecipesPageService _recipesPageService;
    private readonly IUserService<Chef> _chefService;
    private readonly INavigationService _navigationService;

    private Chef? _currentChef;

    public XamlRoot? XamlRoot { get; set; }
    public ObservableCollection<RecipeCategory> RecipeCategories { get; set; } = new();

    public RecipeFormViewModel(IRecipesPageService recipesPageService, IUserService<Chef> chefService, INavigationService navigationService) {
        _recipesPageService = recipesPageService;
        _chefService = chefService;
        _navigationService = navigationService;

        OnInit();
    }

    public async void OnInit() {
        _currentChef = await _chefService.GetByEmail(App.CurrentUserAccount!.Email!);

        List<RecipeCategory> recipeCategories = await _recipesPageService.GetAllRecipeCategories();
        foreach (var category in recipeCategories) {
            if (!RecipeCategories.Any(e => e?.Id == category.Id)) RecipeCategories.Add(category);
        }
    }

    [RelayCommand]
    private async Task AddNewRecipeCategory(RecipeForm recipe) {
        try {
            if (!(RecipeCategories.Count <= 1)) {
                if (RecipeCategories.Any(e => e?.Name == recipe.NewRecipeCategory.Name)) {
                    throw new Exception("Category Already Exists");
                }
            }
        } catch (Exception ex) {
            ShowErrorsDialog dialog = App.GetService<IContentDialogFactory>()
            .ConfigureDialog<ShowErrorsDialog>(XamlRoot!);
            dialog.Content = ex.Message;
            await dialog.ShowAsync();
            return;
        }
        RecipeCategory recipeCategory = await _recipesPageService.AddNewRecipeCategory(recipe.NewRecipeCategory);
        RecipeCategories.Add(recipeCategory);
        recipe.NewRecipeCategory = new();
    }

    [RelayCommand]
    private async Task EditRecipeCategory(RecipeForm recipe) {
        RecipeCategories.Remove(recipe.SelectedRecipeCategory!);
        RecipeCategory? updateRecipeCategory = await _recipesPageService.EditRecipeCategory(recipe.NewRecipeCategory);
        RecipeCategories.Add(updateRecipeCategory);
        recipe.NewRecipeCategory = new();
    }

    [RelayCommand]
    private async Task CreateNewRecipe(RecipeForm recipe) {
        recipe.ChefId = _currentChef!.Id;
        Recipe newRecipe = await _recipesPageService.AddNewRecipe(App.GetService<IMapper>().Map<Recipe>(recipe));
        CancelAndGoBack();
    }

    [RelayCommand]
    private async Task EditRecipe(RecipeForm recipe) {
        Recipe UpdatedRecipe = await _recipesPageService.EditRecipe(App.GetService<IMapper>().Map<Recipe>(recipe));
        CancelAndGoBack();
    }

    [RelayCommand]
    private void CancelAndGoBack() {
        _navigationService.GoBack();
    }
}
