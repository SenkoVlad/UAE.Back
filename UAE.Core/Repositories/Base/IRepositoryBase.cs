using System.Linq.Expressions;

namespace UAE.Core.Repositories.Base;

public interface IRepositoryBase<T> where T : class 
{
    Task AddAsync(T entity);

    Task<bool> IsExists(string id);

    Task DeleteByIdAsync(string id);

     Task UpdateAsync(T entity);

    Task<List<T>> GetAllAsync();

    Task SaveAsync(T entity);
    
    Task<T?> GetByQuery(Expression<Func<T, bool>> expression);
}