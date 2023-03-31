namespace FoodDesire.IMS.ViewModels;
public class RecipeIngredientFormViewModel : ObservableRecipient {
    private readonly IRecipesPageService _recipesPageService;

    public ObservableCollection<Ingredient> RawIngredients { get; set; } = new();
    public ObservableCollection<Recipe> RecipeAsIngredients { get; set; } = new();

    public RecipeIngredientFormViewModel(IRecipesPageService recipesPageService) {
        _recipesPageService = recipesPageService;
        OnInit();
    }

    private async void OnInit() {
        List<Ingredient> ingredients = await _recipesPageService.GetAllIngredients();
        ingredients.ForEach(RawIngredients.Add);
        List<Recipe> recipes = await _recipesPageService.GetAllRecipeAsIngredients();
        recipes.ForEach(RecipeAsIngredients.Add);
    }
}
