namespace FoodDesire.Core.Contracts.Services;
public interface IRecipeService {
    Task<Recipe> NewRecipe(Recipe recipe);
    Task<RecipeCategory> NewRecipeCategory(RecipeCategory recipeCategory);
    Task<Recipe> GetRecipeById(int recipeId);
    Task<List<Recipe>> GetAllRecipes();
    Task<List<Recipe>> GetAllRecipeByCategory(string category);
    Task<List<RecipeIngredient>> GetAllRecipeIngredientsOfRecipe(int recipeId);
    Task<Recipe> UpdateRecipe(Recipe recipe);
    Task<Recipe> AddRecipeIngredientToRecipe(int recipeId, RecipeIngredient recipeIngredient);
    Task<bool> RemoveRecipeIngredientById(int recipeIngredientId);
    Task<bool> RemoveRecipeById(int recipeId);
}
