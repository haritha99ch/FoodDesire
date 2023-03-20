namespace FoodDesire.IMS.Contracts.Services;
public interface IEmployeePageService {
    Task<Chef> NewChef();
    Task<Supplier> NewSupplier(Supplier supplier);
    Task<Deliverer> NewDeliverer(Deliverer deliverer);
}
