using System.Collections.Generic;
using System.Threading.Tasks;
using MongoDB.Entities;
using UAE.Core.Repositories.Base;

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
}