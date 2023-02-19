namespace FoodDesire.Core.Services;
public class IngredientService : IIngredientService {
    private readonly IRepository<Ingredient> _ingredientRepository;
    private readonly ITrackingRepository<IngredientCategory> _ingredientCategoryTRepository;
    private readonly IPaymentService _paymentService;

    public IngredientService(
        IRepository<Ingredient> ingredientRepository,
        IPaymentService paymentService,
        ITrackingRepository<IngredientCategory> categoryRepository
        ) {
        _ingredientRepository = ingredientRepository;
        _paymentService = paymentService;
        _ingredientCategoryTRepository = categoryRepository;
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
        Expression<Func<IngredientCategory, bool>> categoryFilter = e => e.Name.Equals(ingredientCategoryName);
        IngredientCategory ingredientCategory = await _ingredientCategoryTRepository.GetOne(categoryFilter);
        return ingredientCategory;
    }


    public async Task<Ingredient> NewIngredient(Ingredient ingredient) {
        Ingredient newIngredient = await _ingredientRepository.Add(ingredient);
        return newIngredient;
    }

    public async Task<List<Ingredient>> GetAllIngredients() {
        List<Ingredient> ingredients = await _ingredientRepository.GetAll();
        return ingredients;
    }

    public async Task<bool> DeleteIngredientCategoryById(int ingredientCategoryId) {
        bool deleted = await _ingredientCategoryTRepository.SoftDelete(ingredientCategoryId);
        await _ingredientCategoryTRepository.SaveChanges();
        return deleted;
    }

    public async Task<Ingredient> GetIngredientByName(string ingredientName) {
        Expression<Func<Ingredient, bool>> ingredientFilter = e => e.Name.Equals(ingredientName);
        Ingredient ingredient = await _ingredientRepository.GetOne(ingredientFilter);
        return ingredient;
    }

    public async Task<Ingredient> GetIngredientById(int ingredientId) {
        Ingredient ingredient = await _ingredientRepository.GetByID(ingredientId);
        return ingredient;
    }

    public async Task<List<Ingredient>> GetAllIngredientsByCategory(string ingredientCategory) {
        Expression<Func<IngredientCategory, bool>> categoryFilter = e => e.Name.Equals(ingredientCategory);
        IngredientCategory category = await _ingredientCategoryTRepository.GetOne(categoryFilter);

        Expression<Func<Ingredient, bool>> filter = e => e.IngredientCategoryId == category.Id;
        Expression<Func<Ingredient, int>> order = e => e.IngredientCategoryId;
        List<Ingredient> ingredients = await _ingredientRepository.Get(filter, order);
        return ingredients;
    }

    public async Task<Supply> NewSupply(Supply supply, decimal value) {
        Ingredient ingredient = await _ingredientRepository.GetByID(supply.IngredientId);
        ingredient.CurrentQuantity += supply.Amount;
        ingredient.CurrentPricePerUnit = Convert.ToDouble(value) / supply.Amount;
        await _ingredientRepository.Update(ingredient);

        Payment payment = await _paymentService.PaymentForSupply(supply, value);
        await _paymentService.SavePayment();

        return payment.Supply!;
    }
}
