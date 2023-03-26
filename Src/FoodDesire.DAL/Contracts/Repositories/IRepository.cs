namespace FoodDesire.DAL.Contracts.Repositories;
public interface IRepository<T> where T : Entity {
    Task<T> Add(T entity);
    Task<List<T>> AddAll(List<T> entities);
    Task<T> GetByID(int? Id);
    Task<T> GetOne(Expression<Func<T, bool>> filter, Func<IQueryable<T>, IIncludableQueryable<T, object?>>? includes = null);
    Task<List<T>> GetAll();
    Task<List<T>> Get(Expression<Func<T, bool>>? filter, Expression<Func<T, object>>? order = null, Func<IQueryable<T>, IIncludableQueryable<T, object?>>? includes = null); //For filtering
    Task<T> Update(T entity);
    Task<bool> Delete(int Id);
}
