namespace FoodDesire.IMS.Core.Services;
public class SuppliesPageService : ISuppliesPageService {
    private readonly ISupplyService _supplyService;

    public SuppliesPageService(ISupplyService supplyService) {
        _supplyService = supplyService;
    }

    public async Task<Supply> AcceptSupply(int supplyId, int supplierId) => await _supplyService.AcceptSupply(supplyId, supplierId);

    public async Task<Supply> CompleteSupply(int supplyId, decimal value) => await _supplyService.CompleteSupply(supplyId, value);

    public async Task<List<Supply>> GetAllPendingSupplies() => await _supplyService.GetAllPendingSupplies();

    public async Task<List<Supply>> GetAllSupplies() => await _supplyService.GetAllSupplies();

    public async Task<List<Supply>> GetPendingSuppliesForSupplier(int supplierId) => await _supplyService.GetAllPendingSupplierSupplies(supplierId);

    public async Task<List<Supply>> GetSuppliesForSupplier(int supplierId) => await _supplyService.GetAllSupplierSupplies(supplierId);

    public async Task<Supply> GetSupplyById(int supplyId) => await _supplyService.GetSupplyById(supplyId);
}
