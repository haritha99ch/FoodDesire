namespace FoodDesire.Core;
public interface ISupplierService<Supplier>: IUserService<Supplier> where Supplier : User {
    Task<Supply> NewSupply(Supply supply);
}
