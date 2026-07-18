using JAS.ECommerce.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace JAS.ECommerce.Infrastructure.Persistence.Repositories;

public class GenericRepository<TEntity> : IRepository<TEntity> where TEntity : class
{
    private readonly ECommerceDbContext _context;
    private readonly DbSet<TEntity> _dbSet;

    public GenericRepository(ECommerceDbContext context)
    {
        _context = context;
        _dbSet = context.Set<TEntity>();
    }

    public async Task<TEntity?> GetByIdAsync(int id)
    {
        return await _dbSet.FindAsync(id);
    }

    public async Task<IEnumerable<TEntity>> GetAllAsync()
    {
        return await _dbSet.ToListAsync();
    }

    public async Task<TEntity> AddAsync(TEntity entity)
    {
        await _dbSet.AddAsync(entity);
        return entity;
    }

    public void Update(TEntity entity)
    {
        _dbSet.Update(entity);
    }

    public void Delete(TEntity entity)
    {
        _dbSet.Remove(entity);
    }

    public async Task<bool> ExistsAsync(int id)
    {
        return await _dbSet.FindAsync(id) != null;
    }

    public async Task<int> CountAsync()
    {
        return await _dbSet.CountAsync();
    }
}
