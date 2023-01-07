namespace FoodDesire.DAL;
public class Repository<T>: IRepository<T> where T : Entity {
    protected readonly FoodDesireContext _context;
    private DbSet<T> entitySet => _context.Set<T>();

    public Repository(FoodDesireContext context) {
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

    public async Task<T> GetByID(int Id) {
        T? entity = await entitySet.FindAsync(Id);
        if(entity != null) _context.Entry(entity).State = EntityState.Detached;
        return entity!;
    }
    public async Task<T> GetOne(Expression<Func<T, bool>> filter) {
        T? entity = await entitySet.AsNoTracking().SingleOrDefaultAsync(filter);
        return entity!;
    }

    public async Task<List<T>> GetAll() {
        List<T>? entities = await entitySet.AsNoTracking().ToListAsync();
        return entities;
    }

    public async Task<List<T>> Get<T2>(Expression<Func<T, bool>> filter, Expression<Func<T, T2>> order) {
        List<T>? entities = await entitySet.AsNoTracking().Where(filter).OrderBy(order).ToListAsync();
        return entities;
    }

    public async Task<T> Update(T entity) {
        EntityEntry<T>? updatedEntity = entitySet.Update(entity);
        await _context.SaveChangesAsync();
        return updatedEntity.Entity;
    }

    public async Task<bool> Delete(int Id) {
        EntityEntry<T>? entityDeleted = entitySet.Remove(await GetByID(Id));
        await _context.SaveChangesAsync();
        if(await GetByID(Id) == null) return true;
        return false;
    }

    public Task<IDbContextTransaction> StartTransaction() {
        return _context.Database.BeginTransactionAsync();
    }
}