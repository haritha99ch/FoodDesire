namespace FoodDesire.IMS.Core.Contracts.Services;
public interface IEmployeePageService {
    Task<Chef> NewChef();
    Task<Admin> NewAdmin();
    Task<Supplier> NewSupplier();
    Task<Deliverer> NewDeliverer(VehicleType vehicleType, string licenseNo);

    Task<Chef> GetChef(int chefId);
    Task<Supplier> GetSupplier(int supplierId);
    Task<Deliverer> GetDeliverer(int delivererId);

    Task<List<Chef>> GetAllChef();
    Task<List<Supplier>> GetAllSupplier();
    Task<List<Deliverer>> GetAllDeliverer();

    Task<List<User>> GetAllEmployees();

    Task<Payment> MakePaymentForEmployee(Payment payment);
}
