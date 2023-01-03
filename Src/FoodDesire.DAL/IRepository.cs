﻿using FoodDesire.Models;
using System.Linq.Expressions;

namespace FoodDesire.DAL;
public interface IRepository<T> where T : Entity {
    Task<List<T>> GetAll();
    Task<T> GetByID(int Id);
    Task<List<T>> Get<T2>(Expression<Func<T, bool>> filter, Expression<Func<T, T2>> order); //For filtering
    Task<T> Save(T entity);
    Task<T> Add(T entity);
    Task SaveAll(IEnumerable<T> entities);
    Task AddAll(IEnumerable<T> entities);
    Task<bool> Delete(int Id);
}
