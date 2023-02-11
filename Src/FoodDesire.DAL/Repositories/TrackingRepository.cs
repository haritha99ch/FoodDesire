﻿namespace FoodDesire.DAL.Repositories;
public class TrackingRepository<T>: ITrackingRepository<T> where T : TrackedEntity {
    private readonly FoodDesireContext _context;
    private DbSet<T> entitySet => _context.Set<T>();

    public TrackingRepository(FoodDesireContext context) {
        _context = context;
    }

    public async Task<T> Add(T entity) {
        EntityEntry<T> newEntity = await entitySet.AddAsync(entity);
        return newEntity.Entity;
    }

    public async Task<List<T>> AddAll(List<T> entities) {
        await entitySet.AddRangeAsync(entities);
        return entities.ToList();
    }

    public async Task<T> GetByID(int? id) {
        T? entity = await entitySet.SingleAsync(e => !e.Deleted && e.Id == id);
        return entity!;
    }

    public async Task<T> GetOne(Expression<Func<T, bool>> filter, params Func<IQueryable<T>, IQueryable<T>>[]? includes) {
        Expression<Func<T, bool>> TrackedFilter = e => !e.Deleted;
        BinaryExpression? body = Expression.AndAlso(filter.Body, TrackedFilter.Body);
        Expression<Func<T, bool>> entityFilter = Expression.Lambda<Func<T, bool>>(body, filter.Parameters[0]);
        T entity;
        IQueryable<T>? query = entitySet.AsNoTracking().Where(filter);
        if(includes != null) {
            entity = await includes.Aggregate(query, (e, ee) => ee(e)).SingleAsync();
            return entity;
        }
        entity = await query.SingleAsync();
        return entity;
    }

    public async Task<List<T>> GetAll() {
        List<T>? entities = await entitySet.AsNoTracking().Where(e => !e.Deleted).ToListAsync();
        return entities;
    }

    public async Task<List<T>> Get<T2>(Expression<Func<T, bool>> filter, Expression<Func<T, T2>>? order, params Func<IQueryable<T>, IQueryable<T>>[]? includes) {
        Expression<Func<T, bool>> TrackedFilter = e => !e.Deleted;
        BinaryExpression? body = Expression.AndAlso(filter.Body, TrackedFilter.Body);
        Expression<Func<T, bool>> entityFilter = Expression.Lambda<Func<T, bool>>(body, filter.Parameters[0]);
        List<T>? entities = new List<T>();
        IQueryable<T>? query = entitySet.AsNoTracking().Where(filter);
        if(order != null) query.OrderBy(order);
        if(includes != null) {
            entities = await includes.Aggregate(query, (e, ee) => ee(e)).ToListAsync();
            return entities;
        }
        entities = await query.ToListAsync();
        return entities;
    }

    public Task SaveChanges() {
        return _context.SaveChangesAsync();

    }

    public Task<T> Update(T entity) {
        EntityEntry<T>? updatedEntity = entitySet.Update(entity);
        return Task.FromResult(updatedEntity.Entity);
    }
    public async Task<bool> SoftDelete(int Id) {
        T? entity = await GetByID(Id);
        if(entity == null) return false;
        entity.Deleted = true;
        T? updatedEntity = await Update(entity);
        return updatedEntity.Deleted;
    }

    public async Task<bool> Delete(int Id) {
        EntityEntry<T>? entityDeleted = entitySet.Remove(await GetByID(Id));
        return true;
    }

    public async Task<IDbContextTransaction> BeginTransaction() {
        return await _context.Database.BeginTransactionAsync();
    }
}
