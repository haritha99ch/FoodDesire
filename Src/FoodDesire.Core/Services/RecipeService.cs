namespace FoodDesire.Core.Services;
public class RecipeService : IRecipeService {
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
        Expression<Func<RecipeCategory, bool>> filterExpression = e => e.Name == categoryName;

        IQueryable<RecipeCategory> filter(IQueryable<RecipeCategory> e) => e.Where(filterExpression);

        RecipeCategory recipeCategory = await _recipeCategoryRepository.GetOne(filter);
        return recipeCategory;
    }

    public async Task<List<Recipe>> GetAllRecipesByCategoryName(string categoryName) {
        Expression<Func<Recipe, bool>> filterExpression = e => e.RecipeCategory!.Name.Equals(categoryName);
        Expression<Func<Recipe, object>> orderExpression = e => e.RecipeCategoryId;

        IQueryable<Recipe> filter(IQueryable<Recipe> e) => e.Where(filterExpression);
        IOrderedQueryable<Recipe> order(IQueryable<Recipe> e) => e.OrderBy(orderExpression);
        IIncludableQueryable<Recipe, object?> include(IQueryable<Recipe> e) => e.Include(e => e.RecipeCategory);

        List<Recipe> recipes = await _recipeRepository.Get(filter, order, include);
        return recipes;
    }

    public async Task<List<Recipe>> GetAllRecipesByCategoryId(int categoryId) {
        Expression<Func<Recipe, bool>> filterExpression = e => e.RecipeCategoryId == categoryId;
        Expression<Func<Recipe, object>> orderExpression = e => e.RecipeCategoryId;

        IQueryable<Recipe> filter(IQueryable<Recipe> e) => e.Where(filterExpression);
        IOrderedQueryable<Recipe> order(IQueryable<Recipe> e) => e.OrderBy(orderExpression);

        List<Recipe> recipes = await _recipeRepository.Get(filter, order);
        return recipes;
    }

    public async Task<Recipe> RemoveRecipeIngredientById(int recipeId, RecipeIngredient recipeIngredient) {
        Recipe recipe = await _recipeRepository.GetByID(recipeId);
        recipe.RecipeIngredients.Remove(recipeIngredient);
        recipe = await UpdateRecipe(recipe);
        return recipe;
    }

    public async Task<Recipe> UpdateRecipe(Recipe recipe) {
        if (recipe.RecipeIngredients.Count == 0)
            return await _recipeRepository.Update(recipe);
        recipe.MinimumPrice = decimal.Zero;
        recipe.RecipeIngredients.ForEach(async (recipeIngredient) => {
            if (recipeIngredient.Recipe_Id != null) {
                Recipe recipeFromIngredient = await _recipeRepository.GetByID(recipeIngredient.Recipe_Id);
                recipeIngredient.PricePerMultiplier = recipeFromIngredient.FixedPrice;
                recipe.MinimumPrice += (!recipeIngredient.IsRequired) ? 0 : Convert.ToDecimal(Convert.ToDouble(recipeFromIngredient.FixedPrice) * recipeIngredient.Amount);
                return;
            }
            Ingredient ingredient = await _ingredientRepository.GetByID(recipeIngredient.Ingredient_Id);
            recipeIngredient.PricePerMultiplier = await SetMinimumPricePerMultiplier(recipeIngredient);
            recipe.MinimumPrice += (!recipeIngredient.IsRequired) ? 0 : (decimal)(recipeIngredient.Amount * Convert.ToDouble(ingredient.CurrentPricePerUnit));

        });
        if (recipe.FixedPrice < recipe.MinimumPrice)
            recipe.FixedPrice = recipe.MinimumPrice;
        ;
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

    private async Task<decimal> SetMinimumPricePerMultiplier(RecipeIngredient recipeIngredient) {
        Ingredient ingredient = await _ingredientRepository.GetByID(recipeIngredient.Ingredient_Id);
        decimal pricePerMultiplier = (decimal)(recipeIngredient.Amount * Convert.ToDouble(ingredient.CurrentPricePerUnit));

        if (pricePerMultiplier < recipeIngredient.PricePerMultiplier) return recipeIngredient.PricePerMultiplier;
        recipeIngredient.PricePerMultiplier = pricePerMultiplier;
        return recipeIngredient.PricePerMultiplier;
    }
}
