namespace FoodDesire.Core.Services;
public class RecipeService: IRecipeService {
    private readonly IRepository<Recipe> _recipeRepository;
    private readonly IRepository<RecipeCategory> _recipeCategoryRepository;
    private readonly IRepository<Ingredient> _ingredientRepository;

    public RecipeService(
        IRepository<Recipe> recipeRepository,
        IRepository<RecipeCategory> recipeCategoryRepository,
        IRepository<Ingredient> ingredientRepository
        ) {
        _recipeRepository = recipeRepository;
        _recipeCategoryRepository = recipeCategoryRepository;
        _ingredientRepository = ingredientRepository;
    }

    public async Task<Recipe> NewRecipe(Recipe recipe) {
        Recipe newRecipe = await _recipeRepository.Add(recipe);
        newRecipe = await UpdateRecipe(newRecipe);
        return newRecipe;
    }

    public async Task<Recipe> AddRecipeIngredientToRecipe(
        int recipeId, RecipeIngredient recipeIngredient
        ) {
        Recipe recipe = await _recipeRepository.GetByID(recipeId);
        recipe.RecipeIngredients.Add(recipeIngredient);
        recipe = await UpdateRecipe(recipe);
        return recipe;
    }

    public async Task<Recipe> GetRecipeById(int recipeId) {
        Recipe recipe = await _recipeRepository.GetByID(recipeId);
        return recipe;
    }

    public async Task<List<Recipe>> GetAllRecipes() {
        List<Recipe> recipes = await _recipeRepository.GetAll();
        return recipes;
    }

    public async Task<List<RecipeCategory>> GetAllRecipeCategories() {
        List<RecipeCategory> recipeCategories = await _recipeCategoryRepository.GetAll();
        return recipeCategories;
    }

    public async Task<RecipeCategory> GetRecipeCategoryById(int categoryId) {
        RecipeCategory recipeCategory = await _recipeCategoryRepository.GetByID(categoryId);
        return recipeCategory;
    }

    public async Task<RecipeCategory> GetRecipeCategoryByName(string categoryName) {
        Expression<Func<RecipeCategory, bool>> filter = e => e.Name == categoryName;
        RecipeCategory recipeCategory = await _recipeCategoryRepository.GetOne(filter);

        return recipeCategory;
    }

    public async Task<List<Recipe>> GetAllRecipesByCategoryName(string categoryName) {
        Expression<Func<RecipeCategory, bool>> categoryFilter = e => e.Name == categoryName;

        RecipeCategory foodCategory = await _recipeCategoryRepository.GetOne(categoryFilter);
        int categoryId = foodCategory.Id;

        Expression<Func<Recipe, bool>> filter = e => e.RecipeCategoryId == categoryId;
        Expression<Func<Recipe, int>> order = e => e.RecipeCategoryId;

        List<Recipe> recipes = await _recipeRepository.Get(filter, order);
        return recipes;
    }

    public async Task<List<Recipe>> GetAllRecipesByCategoryId(int categoryId) {
        Expression<Func<Recipe, bool>> filter = e => e.RecipeCategoryId == categoryId;
        Expression<Func<Recipe, int>> order = e => e.RecipeCategoryId;

        List<Recipe> recipes = await _recipeRepository.Get(filter, order);
        return recipes;
    }

    public async Task<List<RecipeIngredient>> GetAllRecipeIngredientsForRecipe(int recipeId) {

        return null;
    }

    public async Task<Recipe> RemoveRecipeIngredientById(int recipeId, int recipeIngredientId) {
        Recipe recipe = await _recipeRepository.GetByID(recipeId);
        RecipeIngredient? ingredientToRemove = recipe.RecipeIngredients.FirstOrDefault(e => e.Id == recipeIngredientId);
        if(ingredientToRemove != null) {
            recipe.RecipeIngredients.Remove(ingredientToRemove);
        }
        recipe = await UpdateRecipe(recipe);
        return recipe;
    }

    public async Task<Recipe> UpdateRecipe(Recipe recipe) {
        if(recipe.RecipeIngredients.Count == 0) return await _recipeRepository.Update(recipe);
        recipe.FixedPrice = decimal.Zero;
        recipe.MinimumPrice = decimal.Zero;
        recipe.RecipeIngredients.ForEach(async (recipeIngredient) => {
            Ingredient ingredient = await _ingredientRepository.GetByID(recipeIngredient.IngredientId);
            recipeIngredient.PricePerMultiplier = await SetMinimumPricePerMultiplier(recipeIngredient);
            recipe.MinimumPrice += Convert.ToDecimal(recipeIngredient.Amount * ingredient.CurrentPricePerUnit);
        });
        if(recipe.FixedPrice < recipe.MinimumPrice) recipe.FixedPrice = recipe.MinimumPrice; ;
        return await _recipeRepository.Update(recipe);
    }

    public async Task<bool> RemoveRecipeById(int recipeId) {
        bool recipeDeleted = await _recipeRepository.Delete(recipeId);
        return recipeDeleted;
    }

    public async Task<RecipeCategory> NewRecipeCategory(RecipeCategory recipeCategory) {
        RecipeCategory newRecipeCategory = await _recipeCategoryRepository.Add(recipeCategory);
        return newRecipeCategory;
    }

    public async Task<decimal> SetMinimumPricePerMultiplier(RecipeIngredient recipeIngredient) {
        Ingredient ingredient = await _ingredientRepository.GetByID(recipeIngredient.IngredientId);
        decimal pricePerMultipler = Convert.ToDecimal(recipeIngredient.Amount * ingredient.CurrentPricePerUnit);

        if(pricePerMultipler < recipeIngredient.PricePerMultiplier) return recipeIngredient.PricePerMultiplier;
        recipeIngredient.PricePerMultiplier = pricePerMultipler;
        return recipeIngredient.PricePerMultiplier;
    }

}
