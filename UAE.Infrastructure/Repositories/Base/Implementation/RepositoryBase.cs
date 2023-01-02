using System.Linq.Expressions;
using MongoDB.Driver.Linq;
using MongoDB.Entities;
using UAE.Core.Repositories.Base;

namespace UAE.Infrastructure.Repositories.Base.Implementation;

public class RepositoryBase<T> : IRepositoryBase<T> where T : Entity
{
    public async Task AddAsync(T entity) => 
        await entity.SaveAsync();

    public async Task<bool> IsExists(string id) => 
        await DB.Queryable<T>().AnyAsync(c => c.ID == id);

    public async Task DeleteByIdAsync(string id) => 
        await DB.DeleteAsync<T>(id);

    public async Task<T> GetByIdAsync(string id) =>
        await DB.Find<T>()
            .Match(b => b.ID == id)
            .ExecuteSingleAsync();

    public async Task<List<T>> GetAllAsync() =>
        await DB.Find<T>()
            .Match(_ => true)
            .ExecuteAsync();

    public async Task SaveAsync(T entity) => 
        await DB.SaveAsync(entity);

    public async Task<T?> GetByQuery(Expression<Func<T, bool>> expression) =>
        await DB
            .Queryable<T>()
            .Where(expression)
            .FirstOrDefaultAsync();

    public async Task UpdateAsync(T entity) =>
        await DB.Update<T>()
            .MatchID(entity.ID)
            .ModifyWith(entity)
            .ExecuteAsync();
}