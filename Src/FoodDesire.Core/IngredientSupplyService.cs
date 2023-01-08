
namespace FoodDesire.Core;
public class IngredientSupplyService: IIngredientSupplyService {
    private readonly ITrackingRepository<Supply> _supplyRepository;
    private readonly ITrackingRepository<Ingredient> _ingredientRepository;
    private readonly PaymentService _paymentService;
    private readonly FoodDesireContext _context;

    public IngredientSupplyService(
        ITrackingRepository<Supply> supplyRepository,
        ITrackingRepository<Ingredient> ingredientRepository,
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
        
        return supply;
    }
}
