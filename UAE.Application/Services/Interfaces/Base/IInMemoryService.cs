namespace UAE.Application.Services.Interfaces.Base;

public interface IInMemoryService<out T> where T : class
{
    Task InitAsync();

    public bool IsInitialized { get; }
    
    T[] Data { get; }
}