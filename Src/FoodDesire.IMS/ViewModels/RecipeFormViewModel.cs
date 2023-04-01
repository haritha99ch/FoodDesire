using CommunityToolkit.Mvvm.Input;

namespace FoodDesire.IMS.ViewModels;
public partial class RecipeFormViewModel : ObservableRecipient {
    private readonly IRecipesPageService _recipesPageService;

    public XamlRoot? XamlRoot { get; set; }
    public ObservableCollection<RecipeCategory> RecipeCategories { get; set; } = new();

    public RecipeFormViewModel(IRecipesPageService recipesPageService) {
        _recipesPageService = recipesPageService;
        OnInit();
    }

    public async void OnInit() {
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
}
