namespace FoodDesire.DAL;
public interface ITrackingRepository<T>: IRepository<T> where T : TrackedEntity {
    Task<List<T>> GetAllTracked();
    Task<bool> SoftDelete(int Id);
    Task SaveChanges();
    Task<IDbContextTransaction> BeginTransaction();
}
