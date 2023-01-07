namespace FoodDesire.Core;
public interface IIngredientSupplyService {
    Task<Ingredient> NewIngredient(Ingredient ingredient);
    Task<Ingredient> GetIngredientById(int ingredientId);
    Task<List<Ingredient>> GetAllIngredients();
    Task<Supply> NewSupply(Supply supply, decimal value);
}
