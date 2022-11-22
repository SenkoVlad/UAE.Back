using MongoDB.Entities;
using UAE.Core.Repositories.Base;

namespace UAE.Infrastructure.Repositories.Base;

public class RepositoryBase : IRepositoryBase<Entity>
{
    public Task Save(Entity entity)
    {
        return entity.SaveAsync();
    }

    Task<Entity> IRepositoryBase<Entity>.GetById(string id)
    {
        return DB.Find<Entity>()
            .Match(b => b.ID == id)
            .ExecuteSingleAsync();
    }

    Task<List<Entity>> IRepositoryBase<Entity>.GetAll()
    {
        return DB.Find<Entity>()
            .Match(_ => true)
            .ExecuteAsync();
    }
}