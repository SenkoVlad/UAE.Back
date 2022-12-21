using MongoDB.Entities;
using UAE.Core.Entities;

namespace UAE.Infrastructure.Data.Init;

public static class InitDatabase
{
    public static async Task InitAsync(string database, string host)
    {
        await DB.InitAsync(database, host);
        await AddIndexes();
        await DB.MigrateAsync();
    }

    private static async Task AddIndexes()
    {
        await DB.Index<Announcement>()
            .Key(a => a.Description, KeyType.Text)
            .CreateAsync();

        await DB.Index<Announcement>()
            .Key(a => a.Title, KeyType.Text)
            .CreateAsync();

        await DB.Index<Announcement>()
            .Key(a => a.CreatedDateTime, KeyType.Ascending)
            .CreateAsync();
        
        await DB.Index<Announcement>()
            .Key(a => a.Category.ID, KeyType.Ascending)
            .CreateAsync();
    }
}