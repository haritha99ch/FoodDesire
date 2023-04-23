namespace FoodDesire.Core.Contracts.Services;
public interface IRecipeService {
    Task<Recipe> NewRecipe(Recipe recipe);
    Task<RecipeCategory> NewRecipeCategory(RecipeCategory recipeCategory);
    Task<Recipe> GetRecipeById(int recipeId);
    Task<Recipe> GetRecipeById(int recipeId, bool menuItems = true);
    Task<List<Recipe>> GetAllRecipesWithCategory();
    Task<List<RecipeCategory>> GetAllRecipeCategories();
    Task<RecipeCategory> GetRecipeCategoryById(int categoryId);
    Task<RecipeCategory> GetRecipeCategoryByName(string categoryName);
    Task<List<Recipe>> GetAllRecipesByCategoryName(string categoryName);
    Task<List<Recipe>> GetAllRecipesByCategoryId(int categoryId);
    Task<Recipe> UpdateRecipe(Recipe recipe);
    Task<bool> RemoveRecipeById(int recipeId);
    Task<RecipeCategory> UpdateRecipeCategory(RecipeCategory recipeCategory);
    Task<List<Recipe>> GetAllRecipeAsIngredients();
    Task<List<Recipe>> SearchRecipes(string value);
    Task<List<Recipe>> GetTop10Recipes();
    Task<List<RecipeReview>> GetAllRecipeRatings();
    Task<List<Recipe>> GetAllRecipesWithCategory(bool menuItems);
    Task<List<Recipe>> SearchRecipes(string value, bool menuItems);
}
