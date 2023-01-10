namespace FoodDesire.Core;
public interface IIngredientService {
    Task<Ingredient> NewIngredient(Ingredient ingredient);
    Task<IngredientCategory> NewIngredientCategory(IngredientCategory ingredientCategory);
    Task<bool> DeleteIngredientCategoryById(int ingredientCategoryId);
    Task<Ingredient> GetIngredientById(int ingredientId);
    Task<List<Ingredient>> GetAllIngredients();
    Task<List<Ingredient>> GetAllIngredientsByCategory(string ingredientCategory);
    Task<Supply> NewSupply(Supply supply, decimal value);
}
