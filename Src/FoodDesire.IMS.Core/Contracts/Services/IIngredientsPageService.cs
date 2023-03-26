namespace FoodDesire.IMS.Core.Contracts.Services;
public interface IIngredientsPageService {
    Task<List<Ingredient>> GetAllIngredients();
    Task<List<IngredientCategory>> GetAllIngredientCategory();
    Task<Ingredient> GetIngredientById(int ingredientId);
    Task<IngredientCategory> AddIngredientCategory(IngredientCategory ingredientCategory);
    Task<IngredientCategory> GetIngredientCategoryById(int ingredientCategoryId);
    Task<Ingredient> AddIngredient(Ingredient ingredient);
    Task<Ingredient> EditIngredient(Ingredient ingredient);
    Task<IngredientCategory> EditIngredientCategory(IngredientCategory ingredientCategory);
    Task<bool> DeleteIngredient(int ingredientId);
    Task<bool> DeleteIngredientCategory(int ingredientCategoryId);
    Task<Supply> RequestIngredient(int ingredientId, double amount);
}
