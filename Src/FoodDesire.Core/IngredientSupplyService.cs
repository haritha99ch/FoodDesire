
namespace FoodDesire.Core;
public class IngredientSupplyService: IIngredientSupplyService {
    private readonly ITrackingRepository<Supply> _supplyRepository;
    private readonly IRepository<Ingredient> _ingredientRepository;
    private readonly PaymentService _paymentService;
    private readonly FoodDesireContext _context;

    public IngredientSupplyService(
        ITrackingRepository<Supply> supplyRepository,
        IRepository<Ingredient> ingredientRepository,
        FoodDesireContext context
,
        PaymentService paymentService) {
        _supplyRepository = supplyRepository;
        _ingredientRepository = ingredientRepository;
        _context = context;
        _paymentService = paymentService;
    }

    public async Task<List<Ingredient>> GetAllIngredients() {
        List<Ingredient> ingredients = await _ingredientRepository.GetAll();
        return ingredients;
    }

    public async Task<Ingredient> GetIngredientById(int ingredientId) {
        Ingredient ingredient = await _ingredientRepository.GetByID(ingredientId);
        return ingredient;
    }

    public async Task<Ingredient> NewIngredient(Ingredient ingredient) {
        Ingredient newIngredient = await _ingredientRepository.Add(ingredient);
        return newIngredient;
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
