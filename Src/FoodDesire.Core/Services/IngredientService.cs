namespace FoodDesire.Core.Services;
public class IngredientService : IIngredientService {
    private readonly IRepository<Ingredient> _ingredientRepository;
    private readonly ITrackingRepository<IngredientCategory> _ingredientCategoryTRepository;
    private readonly ITrackingRepository<Supply> _supplyTRepository;

    public IngredientService(
        IRepository<Ingredient> ingredientRepository,
        ITrackingRepository<IngredientCategory> categoryRepository,
        ITrackingRepository<Supply> supplyTRepository
        ) {
        _ingredientRepository = ingredientRepository;
        _ingredientCategoryTRepository = categoryRepository;
        _supplyTRepository = supplyTRepository;
    }

    public async Task<IngredientCategory> NewIngredientCategory(IngredientCategory ingredientCategory) {
        IngredientCategory newIngredientCategory = await _ingredientCategoryTRepository.Add(ingredientCategory);
        await _ingredientCategoryTRepository.SaveChanges();
        return newIngredientCategory;
    }

    public async Task<List<IngredientCategory>> GetAllIngredientCategories() {
        List<IngredientCategory> ingredientCategories = await _ingredientCategoryTRepository.GetAll();
        return ingredientCategories;
    }

    public async Task<IngredientCategory> GetIngredientCategoryById(int ingredientCategoryId) {
        IngredientCategory ingredientCategory = await _ingredientCategoryTRepository.GetByID(ingredientCategoryId);
        return ingredientCategory;
    }

    public async Task<IngredientCategory> GetIngredientCategoryByName(string ingredientCategoryName) {
        Expression<Func<IngredientCategory, bool>> filterExpression = e => e.Name.Equals(ingredientCategoryName);

        IQueryable<IngredientCategory> filter(IQueryable<IngredientCategory> e) => e.Where(filterExpression);

        IngredientCategory ingredientCategory = await _ingredientCategoryTRepository.GetOne(filter);
        return ingredientCategory;
    }


    public async Task<Ingredient> NewIngredient(Ingredient ingredient) {
        Ingredient newIngredient = await _ingredientRepository.Add(ingredient);
        return newIngredient;
    }

    public async Task<List<Ingredient>> GetAllIngredientsWithCategory() {
        IIncludableQueryable<Ingredient, object?> include(IQueryable<Ingredient> e) => e.Include(i => i.IngredientCategory);

        List<Ingredient> ingredients = await _ingredientRepository.Get(null, null, include);
        return ingredients;
    }

    public async Task<bool> DeleteIngredientCategoryById(int ingredientCategoryId) {
        bool deleted = await _ingredientCategoryTRepository.SoftDelete(ingredientCategoryId);
        await _ingredientCategoryTRepository.SaveChanges();
        return deleted;
    }

    public async Task<Ingredient> GetIngredientByName(string ingredientName) {
        Expression<Func<Ingredient, bool>> filterExpression = e => e.Name.Equals(ingredientName);

        IQueryable<Ingredient> filter(IQueryable<Ingredient> e) => e.Where(filterExpression);

        Ingredient ingredient = await _ingredientRepository.GetOne(filter);
        return ingredient;
    }

    public async Task<Ingredient> GetIngredientById(int ingredientId) {
        Ingredient ingredient = await _ingredientRepository.GetByID(ingredientId);
        return ingredient;
    }

    public async Task<List<Ingredient>> GetAllIngredientsByCategory(string ingredientCategory) {
        Expression<Func<Ingredient, bool>> filterExpression = e => e.IngredientCategory!.Name.Equals(ingredientCategory);
        Expression<Func<Ingredient, object>> orderExpression = e => e.IngredientCategoryId;

        IIncludableQueryable<Ingredient, object?> include(IQueryable<Ingredient> e) => e.Include(e => e.IngredientCategory);
        IQueryable<Ingredient> filter(IQueryable<Ingredient> e) => e.Where(filterExpression);
        IOrderedQueryable<Ingredient> order(IQueryable<Ingredient> e) => e.OrderBy(orderExpression);

        List<Ingredient> ingredients = await _ingredientRepository.Get(filter, order, include);
        return ingredients;
    }

    public async Task<Ingredient> EditIngredient(Ingredient ingredient) {
        Ingredient updatedIngredient = await _ingredientRepository.Update(ingredient);
        return updatedIngredient;
    }

    public async Task<bool> DeleteIngredientById(int ingredientCategoryId) {
        bool deleted = await _ingredientRepository.Delete(ingredientCategoryId);
        return deleted;
    }

    public async Task<Supply> NewSupply(Supply supply) {
        supply = await _supplyTRepository.Add(supply);
        Ingredient ingredient = await _ingredientRepository.GetByID(supply.IngredientId);
        ingredient.InSupply = supply.Amount;
        await _supplyTRepository.SaveChanges();
        await _ingredientRepository.Update(ingredient);
        return supply;
    }

    public async Task<IngredientCategory> EditIngredientCategory(IngredientCategory ingredientCategory) {
        IngredientCategory category = await _ingredientCategoryTRepository.Update(ingredientCategory);
        await _ingredientCategoryTRepository.SaveChanges();
        return category;
    }

    public async Task<List<Ingredient>> SearchIngredients(string searchText) {
        if (string.IsNullOrEmpty(searchText)) return await GetAllIngredientsWithCategory();

        Expression<Func<Ingredient, bool>> filterExpression = e => e.Name.StartsWith(searchText);

        IQueryable<Ingredient> filter(IQueryable<Ingredient> e) => e.Where(filterExpression);
        IIncludableQueryable<Ingredient, object?> include(IQueryable<Ingredient> e) => e.Include(i => i.IngredientCategory);

        List<Ingredient> ingredients = await _ingredientRepository.Get(filter, null, include);
        return ingredients;
    }

    public async Task<List<Ingredient>> GetAllIngredients() {
        List<Ingredient> ingredients = await _ingredientRepository.GetAll();
        return ingredients;
    }
}
