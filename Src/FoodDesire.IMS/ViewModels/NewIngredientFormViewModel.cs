namespace FoodDesire.IMS.ViewModels;
public partial class NewIngredientFormViewModel : ObservableObject {
    private readonly IIngredientsPageService _ingredientsPageService;
    [ObservableProperty]
    private Ingredient? ingredient;

    public NewIngredientFormViewModel(IIngredientsPageService ingredientsPageService) {
        _ingredientsPageService = ingredientsPageService;
    }

    public async Task<Ingredient> CreateIngredient() {
        Ingredient newIngredient = await _ingredientsPageService.AddIngredient(Ingredient!);
        return newIngredient;
    }
}
