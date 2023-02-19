﻿namespace FoodDesire.DAL.Repositories;
public class Repository<T> : IRepository<T> where T : Entity {
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

    public async Task<T> GetByID(int? Id) {
        T? entity = await entitySet.FindAsync(Id);
        return entity!;
    }
    public async Task<T> GetOne(Expression<Func<T, bool>> filter, params Func<IQueryable<T>, IQueryable<T>>[]? includes) {
        T? entity;
        IQueryable<T>? query = entitySet.AsNoTracking().Where(filter);
        if(includes != null) {
            entity = await includes.Aggregate(query, (e, ee) => ee(e)).SingleOrDefaultAsync();
            return entity!;
        }
        entity = await query.SingleOrDefaultAsync();
        return entity!;
    }

    public async Task<List<T>> GetAll() {
        List<T>? entities = await entitySet.AsNoTracking().ToListAsync();
        return entities;
    }

    public async Task<List<T>> Get<T2>(Expression<Func<T, bool>> filter, Expression<Func<T, T2>>? order, params Func<IQueryable<T>, IQueryable<T>>[]? includes) {
        List<T>? entities = new List<T>();
        IQueryable<T>? query = entitySet.Where(filter);
        if(order != null)
            query.OrderBy(order);
        if(includes != null) {
            entities = await includes.Aggregate(query, (e, ee) => ee(e)).ToListAsync();
            return entities;
        }
        entities = await query.ToListAsync();
        return entities;
    }

    public async Task<T> Update(T entity) {
        var updatedEntity = entitySet.Update(entity);
        await _context.SaveChangesAsync();
        return entity;
    }

    public async Task<bool> Delete(int Id) {
        EntityEntry<T>? entityDeleted = entitySet.Remove(await GetByID(Id));
        await _context.SaveChangesAsync();
        if(await GetByID(Id) == null)
            return true;
        return false;
    }
}