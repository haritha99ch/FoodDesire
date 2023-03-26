using FoodDesire.IMS.Core.Models;

namespace FoodDesire.IMS.Core.Contracts.Services;
public interface IHomeService {
    Task<decimal> GetTotalIncome();
    Task<decimal> GetTotalExpense();
    Task<List<Order>> GetPendingOrders();
    Task<List<Supply>> GetRecentSupply();
    Task<InventorySummary> GetInventorySummery();
}
