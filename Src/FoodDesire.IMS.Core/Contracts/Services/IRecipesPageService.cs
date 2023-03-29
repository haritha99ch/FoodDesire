namespace FoodDesire.IMS.Core.Contracts.Services;
public interface IRecipesPageService {
    Task<Recipe> AddNewRecipe(Recipe recipe);
    Task<Recipe> EditRecipe(Recipe recipe);
    Task<bool> DeleteRecipeById(int recipeId);
    Task<List<Recipe>> GetAllRecipes();
    Task<List<Recipe>> GetAllRecipeByCategoryId(int categoryId);
    Task<Recipe> GetRecipeById(int recipeId);
}
