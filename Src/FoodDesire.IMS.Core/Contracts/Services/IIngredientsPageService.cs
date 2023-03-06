namespace FoodDesire.IMS.Core.Contracts.Services;
public interface IIngredientsPageService {
    Task<List<Ingredient>> GetAllIngredients();
    Task<Ingredient> GetIngredientById(int ingredientId);
    Task<IngredientCategory> AddIngredientCategory(IngredientCategory ingredientCategory);
    Task<Ingredient> AddIngredient(Ingredient ingredient);
    Task<Ingredient> EditIngredient(Ingredient ingredient);
    Task<bool> DeleteIngredient(int ingredientId);
}
