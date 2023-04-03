using CommunityToolkit.Mvvm.Input;

namespace FoodDesire.IMS.ViewModels;
public partial class RecipeFormViewModel : ObservableRecipient {
    private readonly IRecipesPageService _recipesPageService;
    private readonly IUserService<Chef> _chefService;

    private Chef? _currentChef;

    public XamlRoot? XamlRoot { get; set; }
    public ObservableCollection<RecipeCategory> RecipeCategories { get; set; } = new();

    public RecipeFormViewModel(IRecipesPageService recipesPageService, IUserService<Chef> chefService) {
        _recipesPageService = recipesPageService;
        _chefService = chefService;

        OnInit();
    }

    public async void OnInit() {
        _currentChef = await _chefService.GetByEmail(App.CurrentUserAccount!.Email!);


        List<RecipeCategory> recipeCategories = await _recipesPageService.GetAllRecipeCategories();
        recipeCategories.ForEach(RecipeCategories.Add);
    }

    [RelayCommand]
    private async Task AddNewRecipeCategory(RecipeForm recipe) {
        try {
            if (RecipeCategories.Any(e => e.Name!.Equals(recipe.NewRecipeCategory.Name))) {
                throw new Exception("Category Already Exists");
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
    }
}
