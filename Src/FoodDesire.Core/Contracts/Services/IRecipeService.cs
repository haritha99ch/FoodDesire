namespace FoodDesire.Core.Contracts.Services;
public interface IRecipeService {
    Task<Recipe> NewRecipe(Recipe recipe);
    Task<RecipeCategory> NewRecipeCategory(RecipeCategory recipeCategory);
    Task<Recipe> GetRecipeById(int recipeId);
    Task<List<Recipe>> GetAllRecipes();
    Task<List<RecipeCategory>> GetAllRecipeCategories();
    Task<RecipeCategory> GetRecipeCategoryById(int categoryId);
    Task<RecipeCategory> GetRecipeCategoryByName(string categoryName);
    Task<List<Recipe>> GetAllRecipesByCategoryName(string categoryName);
    Task<List<Recipe>> GetAllRecipesByCategoryId(int categoryId);
    Task<List<RecipeIngredient>> GetAllRecipeIngredientsForRecipe(int recipeId);
    Task<Recipe> UpdateRecipe(Recipe recipe);
    Task<Recipe> AddRecipeIngredientToRecipe(int recipeId, RecipeIngredient recipeIngredient);
    Task<bool> RemoveRecipeIngredientById(int recipeIngredientId);
    Task<bool> RemoveRecipeById(int recipeId);
}
