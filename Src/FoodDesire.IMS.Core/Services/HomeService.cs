namespace FoodDesire.IMS.Core.Services;
public class HomeService : IHomeService {
    private readonly IIngredientService _ingredientService;
    private readonly IPaymentService _paymentService;
    private readonly IOrderService _orderService;

    public HomeService(
        IIngredientService ingredientService,
        IPaymentService paymentService,
        IOrderService orderService
        ) {
        _ingredientService = ingredientService;
        _paymentService = paymentService;
        _orderService = orderService;
    }

    public async Task<InventorySummary> GetInventorySummery() {
        List<Ingredient> ingredients = await _ingredientService.GetAllIngredients();
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

    public async Task<List<Order>> GetPendingOrders() {
        List<Order> orders = await _orderService.GetPendingOrders();
        return orders;
    }

    public Task<List<Supply>> GetRecentSupply() {
        throw new NotImplementedException();
    }

    public async Task<decimal> GetTotalExpense() {
        List<Payment> payments = await _paymentService.GetExpenses();
        decimal expense = payments.Sum(e => e.Value);
        return expense;
    }

    public async Task<decimal> GetTotalIncome() {
        List<Payment> payments = await _paymentService.GetIncome();
        decimal income = payments.Sum(e => e.Value);
        throw new NotImplementedException();
    }
}
