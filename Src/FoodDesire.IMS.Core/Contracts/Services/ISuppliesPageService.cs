namespace FoodDesire.IMS.Core.Contracts.Services;
public interface ISuppliesPageService {
    Task<List<Supply>> GetAllSupplies();
    Task<List<Supply>> GetAllPendingSupplies();
    Task<Supply> GetSupplyById(int supplyId);
    Task<List<Supply>> GetSuppliesForSupplier(int supplierId);
    Task<List<Supply>> GetAcceptedSuppliesForSupplier(int supplierId);
    Task<Supply> AcceptSupply(int supplyId, int supplierId);
    Task<Supply> CompleteSupply(int supplyId, decimal value);
}
