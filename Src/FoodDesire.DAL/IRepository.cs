
namespace FoodDesire.DAL;
public interface IRepository<T> where T : Entity {
    Task<T> Add(T entity);
    Task<List<T>> AddAll(List<T> entities);
    Task<T> GetByID(int Id);
    Task<T> GetOne(Expression<Func<T, bool>> filter);
    Task<List<T>> GetAll();
    Task<List<T>> Get<T2>(Expression<Func<T, bool>> filter, Expression<Func<T, T2>> order); //For filtering
    Task<T> Update(T entity);
    Task<bool> Delete(int Id);
    Task<IDbContextTransaction> StartTransaction();
}
