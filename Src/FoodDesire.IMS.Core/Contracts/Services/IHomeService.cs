namespace FoodDesire.IMS.Core.Contracts.Services;
public interface IHomeService {
    Task<decimal> GetTotalIncome();
    Task<decimal> GetTotalExpense();
    Task<int> GetPendingOrderCount();
    Task<List<Supply>> GetRecentSupply();
    Task<InventorySummary> GetInventorySummery();
    Task<List<Recipe>> GetTop10Recipes();
    Task<int> GetCompletedOrderCount();
}
