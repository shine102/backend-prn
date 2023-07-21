namespace LibraryManagementBackend.Repositories;

using LibraryManagementBackend.Models;

public interface IRepository<T> where T : Entity
{
    public Task<T?> GetByIdAsync(int id);

    public Task<IEnumerable<T>> GetAllAsync(int? skip = null, int? take = null);

    public Task<int> CountAsync();

    public Task<bool> CreateAsync(T entity);

    public Task<bool> UpdateAsync(T entity);

    public Task<bool> DeleteAsync(T entity);
}