namespace LibraryManagementBackend.Repositories;

using LibraryManagementBackend.Models;
using Microsoft.EntityFrameworkCore;

public abstract class Repository<T> : IRepository<T> where T : Entity
{
    protected readonly LibraryManagementDbContext context;

    protected Repository(LibraryManagementDbContext context)
    {
        this.context = context;
    }

    protected abstract DbSet<T> DbSet { get; }

    public virtual async Task<T?> GetByIdAsync(int id)
    {
        return await this.DbSet.FindAsync(id);
    }

    public virtual async Task<IEnumerable<T>> GetAllAsync(int? skip = null, int? take = null)
    {
        var entities = this.DbSet.AsQueryable();
        if (skip is not null)
        {
            entities = entities.Skip(skip.Value);
        }
        if (take is not null)
        {
            entities = entities.Take(take.Value);
        }
        return await entities.ToListAsync();
    }

    public virtual Task<int> CountAsync()
    {
        return this.DbSet.CountAsync();
    }

    public virtual async Task<bool> CreateAsync(T entity)
    {
        await this.DbSet.AddAsync(entity);
        return await this.context.SaveChangesAsync() > 0;
    }

    public virtual async Task<bool> UpdateAsync(T entity)
    {
        this.DbSet.Update(entity);
        return await this.context.SaveChangesAsync() > 0;
    }

    public virtual async Task<bool> DeleteAsync(T entity)
    {
        this.DbSet.Remove(entity);
        return await this.context.SaveChangesAsync() > 0;
    }
}