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

    private async void OnInit() {
        List<RecipeCategory> recipeCategories = await _recipesPageService.GetAllRecipeCategories();
        recipeCategories.ForEach(RecipeCategories.Add);
    }

    [RelayCommand]
    private async Task AddNewRecipeCategory(RecipeForm recipe) {
        try {
            if (recipe.Categories.Contains(recipe.NewRecipeCategory!)) {
                throw new Exception("Category Already Exists");
            }
        } catch (Exception ex) {
            //TODO: Handel duplicate error
        }
        RecipeCategory recipeCategory = new() {
            Name = recipe.NewRecipeCategory!,
            Description = recipe.NewRecipeCategoryDescription!
        };
        recipeCategory = await _recipesPageService.AddNewRecipeCategory(recipeCategory);
        recipe.RecipeCategories.Add(recipeCategory);
        RecipeCategories.Add(recipeCategory);
        recipe.NewRecipeCategory = null;
        recipe.NewRecipeCategoryDescription = null;
    }

    [RelayCommand]
    private async Task EditRecipeCategory(RecipeForm recipe) {
        RecipeCategory recipeCategory = RecipeCategories.SingleOrDefault(e => e.Id == recipe.RecipeCategoryId)!;
        RecipeCategories.Remove(recipeCategory);
        recipe.RecipeCategories.Remove(recipeCategory);
        recipeCategory.Name = recipe.NewRecipeCategory!;
        recipeCategory.Description = recipe.NewRecipeCategoryDescription!;
        recipeCategory = await _recipesPageService.EditRecipeCategory(recipeCategory);
        RecipeCategories.Add(recipeCategory);
        recipe.RecipeCategories.Add(recipeCategory);
    }
}
