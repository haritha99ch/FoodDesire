using System.Linq;

namespace FoodDesire.IMS.Services;
public class HomeService : IHomeService {
    private readonly IOrderService _orderService;
    private readonly IPaymentService _paymentService;

    public HomeService(IOrderService orderService, IPaymentService paymentService) {
        _orderService = orderService;
        _paymentService = paymentService;
    }

    public async Task<decimal> GetExpense() {
        List<Payment> payments = await _paymentService.GetExpenses();
        decimal expense = payments.Sum(e => e.Value);
        return expense;
    }

    public async Task<decimal> GetIncome() {
        List<Payment> payments = await _paymentService.GetIncome();
        decimal income = payments.Sum(e => e.Value);
        return income;
    }

    public async Task<List<Order>> GetPendingOrders() {
        List<Order> orders = await _orderService.GetPendingOrders();
        return orders;
    }
    public Task<Supply> GetRecentSupply() => throw new NotImplementedException();
}
