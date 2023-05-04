namespace FoodDesire.IMS.Core.Services;
public class HomeService : IHomeService {
    private readonly IIngredientService _ingredientService;
    private readonly IPaymentService _paymentService;
    private readonly IOrderService _orderService;
    private readonly IRecipeService _recipeService;

    public HomeService(
        IIngredientService ingredientService,
        IPaymentService paymentService,
        IOrderService orderService,
        IRecipeService recipeService) {
        _ingredientService = ingredientService;
        _paymentService = paymentService;
        _orderService = orderService;
        _recipeService = recipeService;
    }

    public async Task<InventorySummary> GetInventorySummery() {
        List<Ingredient> ingredients = await _ingredientService.GetAllIngredientsWithCategory();
        double totalCapacity = ingredients.Sum(e => e.MaximumQuantity);
        double totalCurrentQuantity = ingredients.Sum(e => e.CurrentQuantity);
        double availableSpace = totalCapacity - totalCurrentQuantity;
        int lowInventoryCount = ingredients.Count(e => e.CurrentQuantity < e.MaximumQuantity / 4);
        InventorySummary summaryDTO = new() {
            TotalCapacity = totalCapacity,
            TotalCurrentQuantity = totalCurrentQuantity,
            LowInventoryCount = lowInventoryCount
        };
        return summaryDTO;
    }

    public async Task<int> GetPendingOrderCount() {
        int count = await _orderService.GetPendingOrderCount();
        return count;
    }

    public Task<List<Supply>> GetRecentSupply() {
        throw new NotImplementedException();
    }

    public async Task<List<Payment>> GetExpenses() {
        List<Payment> payments = await _paymentService.GetExpenses();
        decimal expense = payments.Sum(e => e.Value);
        return payments;
    }

    public async Task<List<Payment>> GetIncomes() {
        List<Payment> payments = await _paymentService.GetIncome();
        return payments;
    }

    public async Task<List<Recipe>> GetTop10Recipes() {
        List<Recipe> recipes = await _recipeService.GetTop10Recipes();
        return recipes;
    }

    public async Task<int> GetCompletedOrderCount() {
        int count = await _orderService.GetCompletedOrderCount();
        return count;
    }
}
