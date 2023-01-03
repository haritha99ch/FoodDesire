using FoodDesire.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Linq.Expressions;

namespace FoodDesire.DAL;
public class Repository<T>: IRepository<T> where T : Entity {
    protected readonly FoodDesireContext _context;
    public Repository(FoodDesireContext context) {
        _context = context;
    }

    public async Task<T> Add(T entity) {
        EntityEntry<T>? addedEntity = await _context.Set<T>().AddAsync(entity);
        await _context.SaveChangesAsync();
        return addedEntity.Entity;
    }

    public async Task AddAll(IEnumerable<T> entities) {
        await _context.Set<T>().AddRangeAsync(entities);
        await _context.SaveChangesAsync();
    }

    public async Task<bool> Delete(int Id) {
        EntityEntry<T>? entityDeleted = _context.Set<T>().Remove(await GetByID(Id));
        throw new NotImplementedException();
    }

    public async Task<List<T>> Get<T2>(Expression<Func<T, bool>> filter, Expression<Func<T, T2>> order) {
        List<T>? entities = await _context.Set<T>().Where(filter).OrderBy(order).ToListAsync();
        return entities;
    }

    public async Task<List<T>> GetAll() {
        List<T>? entities = await _context.Set<T>().AsNoTracking().ToListAsync();
        return entities;
    }

    public async Task<T> GetByID(int Id) {
        T? entity = await _context.Set<T>().FindAsync(Id);
        return entity!;
    }

    public async Task<T> Save(T entity) {
        EntityEntry<T>? updatedEntity = _context.Set<T>().Update(entity);
        await _context.SaveChangesAsync();
        return updatedEntity.Entity;
    }

    public async Task SaveAll(IEnumerable<T> entities) {
        _context.UpdateRange(entities);
        await _context.SaveChangesAsync();
    }
}
