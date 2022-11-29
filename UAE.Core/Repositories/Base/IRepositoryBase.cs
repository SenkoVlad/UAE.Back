using System.Linq.Expressions;

namespace UAE.Core.Repositories.Base;

public interface IRepositoryBase<T> where T : class 
{
    Task AddAsync(T entity);
    
    Task<T> GetByIdAsync(string id);
    
    Task<List<T>> GetAllAsync();

    Task SaveAsync(T entity);
    
    Task<T?> GetByQuery(Expression<Func<T, bool>> expression);

    Task UpdateAsync(T entity);
}