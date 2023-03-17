namespace FoodDesire.Core.Contracts.Services;
internal interface ISupplyService {
    Task<Supply> CreateSupply(Supply supply);
    Task<List<Supply>> GetAllSupplies();
    Task<Supply> GetSupplyById(int supplyId);
    Task<List<Supply>> GetAllPendingSupplies();
    Task<List<Supply>> GetAllAcceptedSupplies();
    Task<List<Supply>> GetAllCompletedSupplies();
    Task<List<Supply>> GetAllPendingSupplierSupplies(int supplierId);
    Task<List<Supply>> GetAllSupplierSupplies(int supplierId);
    Task<Supply> AcceptSupply(int supplyId, int supplierId);
    Task<Supply> CompleteSupply(int supplyId, decimal value);
}
