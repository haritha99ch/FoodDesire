namespace FoodDesire.Core.Services;
public class RecipeService: IRecipeService {
    private readonly IRepository<Recipe> _recipeRepository;
    private readonly IRepository<RecipeIngredient> _recipeIngredientRepository;
    private readonly IRepository<RecipeCategory> _recipeCategoryRepository;

    public RecipeService(
        IRepository<Recipe> recipeRepository,
        IRepository<RecipeIngredient> recipeIngredientRepository,
        IRepository<RecipeCategory> recipeCategoryRepository
        ) {
        _recipeRepository = recipeRepository;
        _recipeIngredientRepository = recipeIngredientRepository;
        _recipeCategoryRepository = recipeCategoryRepository;
    }

    public async Task<Recipe> NewRecipe(Recipe recipe) {
        Recipe newRecipe = await _recipeRepository.Add(recipe);
        return newRecipe;
    }

    public async Task<Recipe> AddRecipeIngredientToRecipe(
        int recipeId, RecipeIngredient recipeIngredient
        ) {
        Recipe recipe = await _recipeRepository.GetByID(recipeId);
        recipe.RecipeIngredients.Add(recipeIngredient);
        recipe = await _recipeRepository.Update(recipe);
        return recipe;
    }

    public async Task<Recipe> GetRecipeById(int recipeId) {
        Recipe recipe = await _recipeRepository.GetByID(recipeId);
        List<RecipeIngredient> recipeIngredients = await GetAllRecipeIngredientsOfRecipe(recipeId);
        recipe.RecipeIngredients = recipeIngredients;
        return recipe;
    }

    public async Task<List<Recipe>> GetAllRecipes() {
        List<Recipe> recipes = await _recipeRepository.GetAll();
        return recipes;
    }

    public async Task<List<Recipe>> GetAllRecipeByCategory(string category) {
        Expression<Func<RecipeCategory, bool>> categoryFilter = e => e.Name == category;

        RecipeCategory foodCategory = await _recipeCategoryRepository.GetOne(categoryFilter);
        int categoryId = foodCategory.Id;

        Expression<Func<Recipe, bool>> filter = e => e.FoodCategoryId == categoryId;
        Expression<Func<Recipe, int>> order = e => e.FoodCategoryId;

        List<Recipe> recipes = await _recipeRepository.Get(filter, order);
        return recipes;
    }

    public async Task<List<RecipeIngredient>> GetAllRecipeIngredientsOfRecipe(int recipeId) {
        Expression<Func<RecipeIngredient, bool>> filter = e => e.RecipeId == recipeId;
        Expression<Func<RecipeIngredient, bool>> order = e => e.IsRequired;

        List<RecipeIngredient> recipeIngredients = await _recipeIngredientRepository.Get(filter, order);
        return recipeIngredients;
    }

    public async Task<bool> RemoveRecipeIngredientById(int recipeIngredientId) {
        bool recipeIngredientDeleted = await _recipeIngredientRepository.Delete(recipeIngredientId);
        return recipeIngredientDeleted;
    }

    public async Task<Recipe> UpdateRecipe(Recipe recipe) {
        Recipe updatedRecipe = await _recipeRepository.Update(recipe);
        return updatedRecipe;
    }

    public async Task<bool> RemoveRecipeById(int recipeId) {
        bool recipeDeleted = await _recipeRepository.Delete(recipeId);
        return recipeDeleted;
    }

    public async Task<RecipeCategory> NewRecipeCategory(RecipeCategory recipeCategory) {
        RecipeCategory newRecipeCategory = await _recipeCategoryRepository.Add(recipeCategory);
        return newRecipeCategory;
    }
}
