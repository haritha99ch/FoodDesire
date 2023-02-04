namespace FoodDesire.Core;
public class IngredientService: IIngredientService {
    private readonly ITrackingRepository<Supply> _supplyRepository;
    private readonly IRepository<Ingredient> _ingredientRepository;
    private readonly ITrackingRepository<IngredientCategory> _ingredientCategoryTRepository;
    private readonly IPaymentService _paymentService;

    public IngredientService(
        ITrackingRepository<Supply> supplyRepository,
        IRepository<Ingredient> ingredientRepository,
        IPaymentService paymentService,
        ITrackingRepository<IngredientCategory> categoryRepository
        ) {
        _supplyRepository = supplyRepository;
        _ingredientRepository = ingredientRepository;
        _paymentService = paymentService;
        _ingredientCategoryTRepository = categoryRepository;
    }

    public async Task<Ingredient> NewIngredient(Ingredient ingredient) {
        Ingredient newIngredient = await _ingredientRepository.Add(ingredient);
        return newIngredient;
    }

    public async Task<IngredientCategory> NewIngredientCategory(IngredientCategory ingredientCategory) {
        IngredientCategory newIngredientCategory = await _ingredientCategoryTRepository.Add(ingredientCategory);
        await _ingredientCategoryTRepository.SaveChanges();
        return newIngredientCategory;
    }

    public async Task<bool> DeleteIngredientCategoryById(int ingredientCategoryId) {
        bool deleted = await _ingredientCategoryTRepository.SoftDelete(ingredientCategoryId);
        await _ingredientCategoryTRepository.SaveChanges();
        return deleted;
    }

    public async Task<List<Ingredient>> GetAllIngredients() {
        List<Ingredient> ingredients = await _ingredientRepository.GetAll();
        return ingredients;
    }

    public async Task<List<Ingredient>> GetAllIngredientsByCategory(string ingredientCategory) {
        Expression<Func<IngredientCategory, bool>> categoryFilter = e => e.Name.Equals(ingredientCategory);
        IngredientCategory category = await _ingredientCategoryTRepository.GetOne(categoryFilter);

        Expression<Func<Ingredient, bool>> filter = e => e.IngredientCategoryId == category.Id;
        Expression<Func<Ingredient, int>> order = e => e.IngredientCategoryId;
        List<Ingredient> ingredients = await _ingredientRepository.Get(filter, order);
        return ingredients;
    }

    public async Task<Ingredient> GetIngredientById(int ingredientId) {
        Ingredient ingredient = await _ingredientRepository.GetByID(ingredientId);
        return ingredient;
    }

    public async Task<Supply> NewSupply(Supply supply, decimal value) {
        Payment payment = new();
        using(IDbContextTransaction? transaction = await _supplyRepository.BeginTransaction()) {
            payment = await _paymentService.PaymentForSupply(supply, value);
            await _paymentService.SavePayment();
            Ingredient ingredient = await _ingredientRepository.GetByID(supply.IngredientId);
            ingredient.CurrentQuantity += supply.Amount;
            ingredient.CurrentPricePerUnit = Convert.ToDouble(value) / supply.Amount;
            ingredient = await _ingredientRepository.Update(ingredient);
            await transaction.CommitAsync();

        }
        return payment.Supply!;
    }

}
