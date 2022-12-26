using UAE.Application.Services.Interfaces.Base;
using UAE.Core.DataModels;
using UAE.Core.Repositories.Base;

namespace UAE.Application.Services.Implementations;

internal sealed class CurrencyInMemoryService : IInMemoryService<Currency>
{
    private readonly IRepositoryBase<Currency> _currencyRepository;
    
    private bool _isInitialized;

    public CurrencyInMemoryService(IRepositoryBase<Currency> currencyRepository)
    {
        _currencyRepository = currencyRepository;
        _isInitialized = false;
    }

    public async Task InitAsync()
    {
        if (!_isInitialized)
        {
            Data = (await _currencyRepository.GetAllAsync()).ToArray();
            _isInitialized = true;
        }
    }

    bool IInMemoryService<Currency>.IsInitialized => _isInitialized;

    public Currency[] Data { get; private set; } = Array.Empty<Currency>();
}