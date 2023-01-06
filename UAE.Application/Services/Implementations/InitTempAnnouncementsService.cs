using UAE.Application.Services.Interfaces;

namespace UAE.Application.Services.Implementations;

public class InitTempAnnouncementsService
{
    private readonly ICategoryInMemory _categoryInMemory;

    public InitTempAnnouncementsService(ICategoryInMemory categoryInMemory)
    {
        _categoryInMemory = categoryInMemory;
    }

    public async Task InitAsync()
    {
        var categories = _categoryInMemory.CategoryWithParentPathModels;
        
        
    }
}