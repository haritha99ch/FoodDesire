namespace FoodDesire.IMS.Core.Services;
public class IngredientsPageService : IIngredientsPageService {
    private readonly IIngredientService _ingredientService;

    public IngredientsPageService(IIngredientService ingredientService) {
        _ingredientService = ingredientService;
    }

    public async Task<Ingredient> AddIngredient(Ingredient ingredient) => await _ingredientService.NewIngredient(ingredient);

    public async Task<IngredientCategory> AddIngredientCategory(IngredientCategory ingredientCategory) =>
        await _ingredientService.NewIngredientCategory(ingredientCategory);

    public async Task<bool> DeleteIngredient(int ingredientId) => await _ingredientService.DeleteIngredientById(ingredientId);

    public async Task<Ingredient> EditIngredient(Ingredient ingredient) => await _ingredientService.EditIngredient(ingredient);

    public async Task<List<Ingredient>> GetAllIngredients() => await _ingredientService.GetAllIngredients();

    public async Task<Ingredient> GetIngredientById(int ingredientId) => await _ingredientService.GetIngredientById(ingredientId);
}
