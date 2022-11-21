namespace UAE.Core.Repositories.Base;

public interface IRepositoryBase<T> where T : class 
{
    public Task Save(T entity);
    
    public Task<T> GetById(string id);
    
    public Task<List<T>> GetAll();
}