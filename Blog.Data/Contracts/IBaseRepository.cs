namespace Blog.Data.Contracts;

public interface IBaseRepository<TEntity> where TEntity : class
{
    IQueryable<TEntity> GetAll();
    Task<TEntity?> GetByIdAsync<TId>(TId id);

    Task AddAsync(TEntity entity);
    Task UpdateAsync(TEntity entity);
    Task DeleteAsync(TEntity entity);

    Task SaveChangesAsync();
}
