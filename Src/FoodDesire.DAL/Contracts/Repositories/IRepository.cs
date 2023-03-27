namespace FoodDesire.DAL.Contracts.Repositories;
public interface IRepository<T> where T : Entity {
    Task<T> Add(T entity);
    Task<List<T>> AddAll(List<T> entities);
    Task<T> GetByID(int? Id);
    Task<T> GetOne(Func<IQueryable<T>, IQueryable<T>> filter, Func<IQueryable<T>, IIncludableQueryable<T, object?>>? include = null);
    Task<List<T>> GetAll();
    Task<List<T>> Get(Func<IQueryable<T>, IQueryable<T>>? filter, Func<IQueryable<T>, IOrderedQueryable<T>>? order = null, Func<IQueryable<T>, IIncludableQueryable<T, object?>>? include = null); //For filtering
    Task<T> Update(T entity);
    Task<bool> Delete(int Id);
}
