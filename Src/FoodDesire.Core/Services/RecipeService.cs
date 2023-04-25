namespace FoodDesire.Core.Services;
public class RecipeService : IRecipeService {
    private readonly IRepository<Recipe> _recipeRepository;
    private readonly IRepository<RecipeCategory> _recipeCategoryRepository;
    private readonly IRepository<Ingredient> _ingredientRepository;
    private readonly IRepository<RecipeReview> _recipeRatingRepository;

    public RecipeService(
        IRepository<Recipe> recipeRepository,
        IRepository<RecipeCategory> recipeCategoryRepository,
        IRepository<Ingredient> ingredientRepository,
        IRepository<RecipeReview> recipeRatingRepository
        ) {
        _recipeRepository = recipeRepository;
        _recipeCategoryRepository = recipeCategoryRepository;
        _ingredientRepository = ingredientRepository;
        _recipeRatingRepository = recipeRatingRepository;
    }

    public async Task<Recipe> NewRecipe(Recipe recipe) {
        Recipe newRecipe = await _recipeRepository.Add(recipe);
        newRecipe = await UpdateRecipe(newRecipe);
        return newRecipe;
    }

    public async Task<Recipe> GetRecipeById(int recipeId) {
        Expression<Func<Recipe, bool>> filterExpression = e => e.Id == recipeId;

        IQueryable<Recipe> filter(IQueryable<Recipe> e) => e.Where(filterExpression);
        IIncludableQueryable<Recipe, object> include(IQueryable<Recipe> e) =>
            e.Include(e => e.Images!).Include(e => e.RecipeCategory!).Include(e => e.Chef!).ThenInclude(e => e.User!);

        Recipe recipe = await _recipeRepository.GetOne(filter, include);
        return recipe;
    }

    public async Task<Recipe> GetRecipeById(int recipeId, bool menuItems) {
        Expression<Func<Recipe, bool>> filterExpression = e => e.Id == recipeId;

        IQueryable<Recipe> filter(IQueryable<Recipe> e) => e.Where(filterExpression);
        IIncludableQueryable<Recipe, object> include(IQueryable<Recipe> e) =>
            e.Include(e => e.Images!).Include(e => e.RecipeCategory!);

        Recipe recipe = await _recipeRepository.GetOne(filter, include);
        return recipe;
    }

    public async Task<List<Recipe>> GetAllRecipesWithCategory() {
        IIncludableQueryable<Recipe, object> include(IQueryable<Recipe> e) =>
            e.Include(e => e.RecipeCategory!).Include(e => e.Images.Take(1));

        List<Recipe> recipes = await _recipeRepository.Get(null, null, include);
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
        Expression<Func<Recipe, bool>> filterExpression = e => e.RecipeCategory!.Name!.Equals(categoryName);
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
        IIncludableQueryable<Recipe, object> include(IQueryable<Recipe> e) =>
            e.Include(e => e.RecipeCategory!).Include(e => e.Images);

        List<Recipe> recipes = await _recipeRepository.Get(filter, order, include);
        return recipes;
    }

    public async Task<Recipe> UpdateRecipe(Recipe recipe) {
        if (recipe.RecipeIngredients.Count == 0)
            return await _recipeRepository.Update(recipe);
        decimal profit = 0;
        if (!(recipe.MinimumPrice == 0 || recipe.FixedPrice == 0)) {
            profit = (recipe.FixedPrice - recipe.MinimumPrice) / recipe.MinimumPrice * 100;
        }
        recipe.MinimumPrice = decimal.Zero;
        foreach (var recipeIngredient in recipe.RecipeIngredients) {
            if (recipeIngredient.Recipe_Id != null) {
                Recipe recipeFromIngredient = await _recipeRepository.GetByID(recipeIngredient.Recipe_Id);
                recipeIngredient.Value = (decimal)((double)recipeFromIngredient.FixedPrice * recipeIngredient.Amount);
                recipeIngredient.Measurement = Measurement.Each;
                recipeIngredient.PricePerMultiplier = Math.Round(recipeFromIngredient.FixedPrice * (1 + profit / 100), 2);
                recipe.MinimumPrice += (!recipeIngredient.IsRequired) ? 0 : Convert.ToDecimal(Convert.ToDouble(recipeFromIngredient.FixedPrice) * recipeIngredient.Amount);
                continue;
            }
            Ingredient ingredient = await _ingredientRepository.GetByID(recipeIngredient.Ingredient_Id);
            recipeIngredient.Value = (decimal)((double)ingredient.CurrentPricePerUnit * recipeIngredient.Amount);
            recipeIngredient.Measurement = ingredient.Measurement;
            recipeIngredient.PricePerMultiplier = Math.Round(ingredient.CurrentPricePerUnit * (1 + profit / 100), 2);
            recipe.MinimumPrice += (!recipeIngredient.IsRequired) ? 0 : (decimal)(recipeIngredient.Amount * Convert.ToDouble(ingredient.CurrentPricePerUnit));
        }
        if (recipe.FixedPrice < recipe.MinimumPrice)
            recipe.FixedPrice = recipe.MinimumPrice;
        if (profit == 0) {
            recipe = await _recipeRepository.Update(recipe);
            await UpdateRecipe(recipe);
        }
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

    public async Task<RecipeCategory> UpdateRecipeCategory(RecipeCategory recipeCategory) {
        RecipeCategory updatedRecipeCategory = await _recipeCategoryRepository.Update(recipeCategory);
        return updatedRecipeCategory;
    }

    public async Task<List<Recipe>> GetAllRecipeAsIngredients() {
        Expression<Func<Recipe, bool>> filterExpression = e => e.AsIngredient;

        IQueryable<Recipe> filter(IQueryable<Recipe> e) => e.Where(filterExpression);

        List<Recipe> recipes = await _recipeRepository.Get(filter);
        return recipes;
    }

    public async Task<List<Recipe>> SearchRecipes(string value) {
        if (string.IsNullOrEmpty(value)) return await GetAllRecipesWithCategory();
        Expression<Func<Recipe, bool>> filterExpression = e => e.Name.StartsWith(value);

        IIncludableQueryable<Recipe, object> include(IQueryable<Recipe> e) =>
            e.Include(e => e.RecipeCategory!).Include(e => e.Images.Take(1));
        IQueryable<Recipe> filter(IQueryable<Recipe> e) => e.Where(filterExpression);

        List<Recipe> recipes = await _recipeRepository.Get(filter, null, include);
        return recipes;
    }

    public async Task<List<Recipe>> GetTop10Recipes() {
        Expression<Func<Recipe, bool>> filterExpression = e => e.IsMenuItem;

        IQueryable<Recipe> filter(IQueryable<Recipe> e) => e.Where(filterExpression).Take(10);
        IIncludableQueryable<Recipe, object> include(IQueryable<Recipe> e) =>
            e.Include(e => e.RecipeCategory!).Include(e => e.Images.Take(1));
        IOrderedQueryable<Recipe> order(IQueryable<Recipe> e) => e.OrderBy(e => e.Times);

        List<Recipe> recipes = await _recipeRepository.Get(filter, order, include);
        return recipes;
    }

    public async Task<List<RecipeReview>> GetAllRecipeRatings() {
        List<RecipeReview> recipeRatings = await _recipeRatingRepository.GetAll();
        return recipeRatings;
    }

    public async Task<List<Recipe>> GetAllRecipesWithCategory(bool menuItems) {
        Expression<Func<Recipe, bool>> filterExpression = e => e.IsMenuItem;

        IIncludableQueryable<Recipe, object> include(IQueryable<Recipe> e) =>
            e.Include(e => e.RecipeCategory!).Include(e => e.Images.Take(1));
        IQueryable<Recipe> filter(IQueryable<Recipe> e) => e.Where(filterExpression);

        List<Recipe> recipes = await _recipeRepository.Get(filter, null, include);
        return recipes;
    }

    public async Task<List<Recipe>> SearchRecipes(string value, bool menuItems) {
        if (string.IsNullOrEmpty(value)) return await GetAllRecipesWithCategory();
        Expression<Func<Recipe, bool>> filterExpression = e => e.Name.StartsWith(value) && e.IsMenuItem;

        IIncludableQueryable<Recipe, object> include(IQueryable<Recipe> e) =>
            e.Include(e => e.RecipeCategory!).Include(e => e.Images.Take(1));
        IQueryable<Recipe> filter(IQueryable<Recipe> e) => e.Where(filterExpression);
        List<Recipe> recipes = await _recipeRepository.Get(filter, null, include);
        return recipes;
    }
}
