namespace FoodDesire.DAL.Contracts.Repositories;
public interface ITrackingRepository<T>: IRepository<T> where T : TrackedEntity {
    Task<bool> SoftDelete(int Id);
    Task SaveChanges();
    Task<IDbContextTransaction> BeginTransaction();
}
