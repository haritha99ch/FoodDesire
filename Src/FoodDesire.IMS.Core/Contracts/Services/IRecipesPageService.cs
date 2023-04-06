namespace FoodDesire.IMS.Core.Contracts.Services;
public interface IRecipesPageService {
    Task<Recipe> AddNewRecipe(Recipe recipe);
    Task<Recipe> EditRecipe(Recipe recipe);
    Task<bool> DeleteRecipeById(int recipeId);
    Task<List<Recipe>> GetAllRecipes();
    Task<List<Recipe>> GetAllRecipeByCategoryId(int categoryId);
    Task<Recipe> GetRecipeById(int recipeId);
    Task<RecipeCategory> GetRecipeCategoryById(int recipeCategoryId);
    Task<RecipeCategory> GetRecipeCategoryByName(string recipeCategoryName);
    Task<List<RecipeCategory>> GetAllRecipeCategories();
    Task<RecipeCategory> AddNewRecipeCategory(RecipeCategory recipeCategory);
    Task<RecipeCategory> EditRecipeCategory(RecipeCategory recipeCategory);
    Task<List<Ingredient>> GetAllIngredients();
    Task<List<Recipe>> GetAllRecipeAsIngredients();
    Task<List<Recipe>> SearchRecipes(string value);
}
