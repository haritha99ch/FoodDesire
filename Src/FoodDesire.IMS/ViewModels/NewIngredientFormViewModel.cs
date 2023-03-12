namespace FoodDesire.IMS.ViewModels;
public partial class NewIngredientFormViewModel : ObservableObject {
    private readonly IIngredientsPageService _ingredientsPageService;
    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(CanBeCreated))]
    private Ingredient? ingredient;
    public bool CanBeCreated {
        get {
            // Add your validation logic here
            if (string.IsNullOrEmpty(Ingredient.Name)) return false;
            if (string.IsNullOrEmpty(Ingredient.Description)) return false;
            if (Ingredient.MaximumQuantity <= 0) return false;

            // If all validation checks pass, return true
            return true;
        }
    }
    public NewIngredientFormViewModel(IIngredientsPageService ingredientsPageService) {
        _ingredientsPageService = ingredientsPageService;
    }


    public async Task<Ingredient> CreateIngredient() {
        Ingredient newIngredient = await _ingredientsPageService.AddIngredient(Ingredient!);
        return newIngredient;
    }
}
