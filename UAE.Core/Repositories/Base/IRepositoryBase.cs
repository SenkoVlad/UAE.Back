namespace UAE.Core.Repositories.Base;

public interface IRepositoryBase<T> where T : class 
{
    public Task AddAsync(T entity);
    
    public Task<T> GetByIdAsync(string id);
    
    public Task<List<T>> GetAllAsync();
}