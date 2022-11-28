using System.Linq.Expressions;

namespace UAE.Core.Repositories.Base;

public interface IRepositoryBase<T> where T : class 
{
    public Task AddAsync(T entity);
    
    public Task<T> GetByIdAsync(string id);
    
    public Task<List<T>> GetAllAsync();

    public Task SaveAsync(T entity);
    
    Task<T?> GetByQuery(Expression<Func<T, bool>> expression);
}