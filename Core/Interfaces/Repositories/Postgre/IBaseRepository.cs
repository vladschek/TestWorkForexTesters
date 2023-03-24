﻿namespace Core.Interfaces.Repositories.Postgre;

public interface IBaseRepository<T> where T : class
{
    Task<IEnumerable<T>> GetAllAsync(bool trackChanges = false);
    Task<T?> GetByIdAsync(int id);
    Task<T> CreateAsync(T entity);
    Task<T> UpdateAsync(T entity);
    Task DeleteAsync(int id);
    Task<int> SaveChangesAsync();
}
