
namespace FoodDesire.Core;
public class IngredientSupplyService: IIngredientSupplyService {
    private readonly IRepository<Supply> _supplyRepository;
    private readonly IRepository<Ingredient> _ingredientRepository;
    private readonly PaymentService _paymentService;
    private readonly FoodDesireContext _context;

    public IngredientSupplyService(
        IRepository<Supply> supplyRepository,
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
        Supply newSupply = new();
        using(IDbContextTransaction? transaction = await _ingredientRepository.StartTransaction()) {
            newSupply = await _supplyRepository.Add(supply);
            Ingredient ingredient = await _ingredientRepository.GetByID(newSupply.IngredientId);
            ingredient.CurrentQuantity += newSupply.Amount;
            Payment paymentForSupply = await _paymentService.PaymentForSupply(newSupply, value);
            ingredient.CurrentPricePerUnit = Convert.ToDouble(value) / supply.Amount;
            Ingredient updatedIngredient = await _ingredientRepository.Update(ingredient);
            transaction.Commit();
        }
        return newSupply;
    }
}
