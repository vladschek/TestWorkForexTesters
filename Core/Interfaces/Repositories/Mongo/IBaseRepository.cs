namespace Core.Interfaces.Repositories.Mongo
{
    public interface IBaseRepository<T> where T : class
    {
        Task<IEnumerable<T>> GetAllAsync();
        Task<T> GetByIdAsync(int id);
        Task CreateAsync(T entity);
        Task<bool> UpdateAsync(T entity);
    }
}
