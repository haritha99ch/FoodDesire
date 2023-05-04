namespace FoodDesire.IMS.Core.Contracts.Services;
public interface IHomeService {
    Task<List<Payment>> GetIncomes();
    Task<List<Payment>> GetExpenses();
    Task<int> GetPendingOrderCount();
    Task<List<Supply>> GetRecentSupply();
    Task<InventorySummary> GetInventorySummery();
    Task<List<Recipe>> GetTop10Recipes();
    Task<int> GetCompletedOrderCount();
}
