namespace JAS.ECommerce.Domain.Interfaces;

public interface IRepository<TEntity> where TEntity : class
{
    Task<TEntity?> GetByIdAsync(int id);
    Task<IEnumerable<TEntity>> GetAllAsync();
    Task<TEntity> AddAsync(TEntity entity);
    void Update(TEntity entity);
    void Delete(TEntity entity);
    Task<bool> ExistsAsync(int id);
    Task<int> CountAsync();
}
