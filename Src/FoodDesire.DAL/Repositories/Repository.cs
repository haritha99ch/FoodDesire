namespace FoodDesire.DAL.Repositories;
public class Repository<T> : IRepository<T> where T : Entity {
    protected readonly ApplicationDbContext _context;
    private DbSet<T> entitySet => _context.Set<T>();

    public Repository(ApplicationDbContext context) {
        _context = context;
    }

    public async Task<T> Add(T entity) {
        EntityEntry<T>? addedEntity = await entitySet.AddAsync(entity);
        await _context.SaveChangesAsync();
        return addedEntity.Entity;
    }

    public async Task<List<T>> AddAll(List<T> entities) {
        await entitySet.AddRangeAsync(entities);
        await _context.SaveChangesAsync();
        return entities.ToList();
    }

    public async Task<T> GetByID(int? Id) {
        T? entity = await entitySet.FindAsync(Id);
        return entity!;
    }

    public async Task<List<T>> GetAll() {
        List<T>? entities = await entitySet.AsNoTracking().ToListAsync();
        return entities;
    }

    public async Task<T> Update(T entity) {
        EntityEntry<T>? updatedEntity;
        try {
            updatedEntity = entitySet.Update(entity);
        } catch (Exception) {
            _context.ChangeTracker.Clear();
            updatedEntity = entitySet.Update(entity);
        }
        await _context.SaveChangesAsync();
        return updatedEntity.Entity;
    }

    public async Task<bool> Delete(int Id) {
        T? entity = await GetByID(Id);
        if (entity == null) return false;
        EntityEntry<T>? entityDeleted = entitySet.Remove(entity);
        await _context.SaveChangesAsync();
        if (await GetByID(Id) == null)
            return true;
        return false;
    }

    public async Task<T> GetOne
        (Func<IQueryable<T>, IQueryable<T>> filter,
        Func<IQueryable<T>, IIncludableQueryable<T, object?>>? include = null
        ) {
        IQueryable<T>? query = entitySet.AsNoTracking();
        query = filter(query);
        if (include != null) query = include(query);
        T? entity = await query.FirstOrDefaultAsync();
        return entity!;
    }

    public async Task<List<T>> Get(
        Func<IQueryable<T>, IQueryable<T>>? filter,
        Func<IQueryable<T>, IOrderedQueryable<T>>? order = null,
        Func<IQueryable<T>, IIncludableQueryable<T, object?>>? include = null
        ) {
        IQueryable<T> query = entitySet.AsNoTracking();

        query = include?.Invoke(query) ?? query;
        query = filter?.Invoke(query) ?? query;
        query = order?.Invoke(query) ?? query;

        List<T>? entities = await query.ToListAsync();
        return entities;
    }
}
