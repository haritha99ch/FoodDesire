﻿namespace FoodDesire.DAL.Contracts.Repositories;
public interface IRepository<T> where T : Entity {
    Task<T> Add(T entity);
    Task<List<T>> AddAll(List<T> entities);
    Task<T> GetByID(int Id);
    Task<T> GetOne(Expression<Func<T, bool>> filter, params Func<IQueryable<T>, IQueryable<T>>[]? includes);
    Task<List<T>> GetAll();
    Task<List<T>> Get<T2>(Expression<Func<T, bool>> filter, Expression<Func<T, T2>>? order, params Func<IQueryable<T>, IQueryable<T>>[]? includes); //For filtering
    Task<T> Update(T entity);
    Task<bool> Delete(int Id);
}