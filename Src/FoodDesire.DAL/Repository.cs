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

    public async Task AddAll(IEnumerable<T> entities) {
        await entitySet.AddRangeAsync(entities);
        await _context.SaveChangesAsync();
    }

    public async Task<bool> Delete(int Id) {
        EntityEntry<T>? entityDeleted = entitySet.Remove(await GetByID(Id));
        throw new NotImplementedException();
    }

    public async Task<List<T>> Get<T2>(Expression<Func<T, bool>> filter, Expression<Func<T, T2>> order) {
        List<T>? entities = await entitySet.Where(filter).OrderBy(order).ToListAsync();
        return entities;
    }
    public async Task<T> GetOne(Expression<Func<T, bool>> filter) {
        T? entity = await entitySet.SingleOrDefaultAsync(filter);
        return entity!;
    }

    public async Task<List<T>> GetAll() {
        List<T>? entities = await entitySet.AsNoTracking().ToListAsync();
        return entities;
    }

    public async Task<T> GetByID(int Id) {
        T? entity = await entitySet.FindAsync(Id);
        return entity!;
    }

    public async Task<T> Update(T entity) {
        EntityEntry<T>? updatedEntity = entitySet.Update(entity);
        await _context.SaveChangesAsync();
        return updatedEntity.Entity;
    }

    public async Task SaveAll(IEnumerable<T> entities) {
        _context.UpdateRange(entities);
        await _context.SaveChangesAsync();
    }
}
