namespace FoodDesire.Core.Contracts.Services;
public interface ISupplyService {
    Task<Supply> CreateSupply(int ingredientId, double amount);
    Task<List<Supply>> GetAllSupplies();
    Task<Supply> GetSupplyById(int supplyId);
    Task<List<Supply>> GetAllPendingSupplies();
    Task<List<Supply>> GetAllAcceptedSupplies();
    Task<List<Supply>> GetAllCompletedSupplies();
    Task<List<Supply>> GetAllAcceptedSuppliesForSupplier(int supplierId);
    Task<List<Supply>> GetAllSupplierSupplies(int supplierId);
    Task<Supply> AcceptSupply(int supplyId, int supplierId);
    Task<Supply> CompleteSupply(int supplyId, decimal value);
}
