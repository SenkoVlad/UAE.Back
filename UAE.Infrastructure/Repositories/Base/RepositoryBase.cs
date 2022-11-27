using System.Linq.Expressions;
using MongoDB.Entities;
using UAE.Core.Repositories.Base;
using MongoDB.Driver.Linq;

namespace UAE.Infrastructure.Repositories.Base;

public class RepositoryBase<T> : IRepositoryBase<T> where T : Entity
{
    public Task AddAsync(T entity)
    {
        return entity.SaveAsync();
    }

    public Task<T> GetByIdAsync(string id)
    {
        return DB.Find<T>()
            .Match(b => b.ID == id)
            .ExecuteSingleAsync();
    }

    public Task<List<T>> GetAllAsync()
    {
        return DB.Find<T>()
            .Match(_ => true)
            .ExecuteAsync();
    }

    public async Task SaveAsync(T entity)
    {
        await DB.SaveAsync(entity);
    }

    public async Task<T?> GetByQuery(Expression<Func<T, bool>> expression)
    {
        var result = await DB
            .Queryable<T>()
            .Where(expression)
            .FirstOrDefaultAsync();

        return result;
    }
}