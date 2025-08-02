namespace JASData.DomainService;

public interface IJASService<T> where T : class
{
    Task<T> GetByIdAsync(int id);
    Task<T> GetAllAsync();
    Task<T> DeleteASync(int id);
}
