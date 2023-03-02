namespace FoodDesire.IMS.Contracts.Services;
public interface IHomeService {
    Task<List<Order>> GetPendingOrders();
    Task<decimal> GetIncome();
    Task<decimal> GetExpense();
    Task<Supply> GetRecentSupply();
}
