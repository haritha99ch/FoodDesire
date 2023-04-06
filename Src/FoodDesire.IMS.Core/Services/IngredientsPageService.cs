namespace FoodDesire.IMS.Core.Services;
public class IngredientsPageService : IIngredientsPageService {
    private readonly IIngredientService _ingredientService;
    private readonly ISupplyService _supplyService;

    public IngredientsPageService(IIngredientService ingredientService, ISupplyService supplyService) {
        _ingredientService = ingredientService;
        _supplyService = supplyService;
    }

    public async Task<Ingredient> AddIngredient(Ingredient ingredient) => await _ingredientService.NewIngredient(ingredient);

    public async Task<IngredientCategory> AddIngredientCategory(IngredientCategory ingredientCategory) => await _ingredientService.NewIngredientCategory(ingredientCategory);

    public async Task<bool> DeleteIngredient(int ingredientId) => await _ingredientService.DeleteIngredientById(ingredientId);

    public async Task<bool> DeleteIngredientCategory(int ingredientCategoryId) => await _ingredientService.DeleteIngredientCategoryById(ingredientCategoryId);

    public async Task<Ingredient> EditIngredient(Ingredient ingredient) => await _ingredientService.EditIngredient(ingredient);

    public async Task<IngredientCategory> EditIngredientCategory(IngredientCategory ingredientCategory) => await _ingredientService.EditIngredientCategory(ingredientCategory);

    public async Task<List<IngredientCategory>> GetAllIngredientCategory() => await _ingredientService.GetAllIngredientCategories();

    public async Task<List<Ingredient>> GetAllIngredients() => await _ingredientService.GetAllIngredients();

    public async Task<Ingredient> GetIngredientById(int ingredientId) => await _ingredientService.GetIngredientById(ingredientId);

    public async Task<IngredientCategory> GetIngredientCategoryById(int ingredientCategoryId) => await _ingredientService.GetIngredientCategoryById(ingredientCategoryId);

    public async Task<Supply> RequestIngredient(int ingredientId, double amount) => await _supplyService.CreateSupply(ingredientId, amount);

    public async Task<List<Ingredient>> SearchIngredients(string searchText) => await _ingredientService.SearchIngredients(searchText);
}
