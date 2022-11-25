using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Entities;
using UAE.Core.Entities;
using UAE.Core.EntityDataParameters;

namespace UAE.Infrastructure.Data.Init;

public class _001_seed_init_data : IMigration
{
    public async Task UpgradeAsync()
    {
        await AddUsers();
        await AddCategories();
        await AddAnnouncements();
    }

    private async Task AddAnnouncements()
    {
        await DB.DeleteAsync<Announcement>(_ => true);

        var category = await DB.Find<Category>()
            .Match(s => s.Name == "real estate")
            .ExecuteSingleAsync();

        var user = await DB.Find<User>()
            .Match(s => s.Name == "vlad")
            .ExecuteSingleAsync();
        
        var announcement = new Announcement
        {
            Category = new One<Category>()
            {
                ID = category.ID
            },
            Description = "flat 1",
            Parameters = new Dictionary<string, string>()
            {
                {RealEstateParameterNames.Floor, "2"},
                {RealEstateParameterNames.NumberOfBedrooms, "2"},
                {RealEstateParameterNames.Number, "23"},
                {RealEstateParameterNames.BedroomType, "shower"},
                {RealEstateParameterNames.YearOfBuilding, "2012"}
            },
            Title = "flat super flat",
            User = new One<User>()
            {
                ID = user.ID
            },
            CreatedDateTime = DateTime.Now
        };

        await announcement.SaveAsync();
    }

    private static async Task AddUsers()
    {
        await DB.DeleteAsync<User>(_ => true);

        var user = new User
        {
            Name = "vlad",
        };
        
        await user.SaveAsync();
    }

    private static async Task AddCategories()
    {
        await DB.DeleteAsync<Category>(_ => true);

        var readEstateCategory = new Category
        {
            Name = "real estate"
        };

        var flatCategory = new Category
        {
            Name = "flat",
            ID = ObjectId.GenerateNewId().ToString()
        };

        var villaCategory = new Category
        {
            Name = "villa",
            ID = ObjectId.GenerateNewId().ToString()
        };

        var carCategory = new Category
        {
            Name = "car"
        };

        var sportCar = new Category
        {
            Name = "sport car",
            ID = ObjectId.GenerateNewId().ToString()
        };


        carCategory.ChildrenCategories.Add(sportCar);
        await carCategory.SaveAsync();

        readEstateCategory.ChildrenCategories.AddRange(new[] {villaCategory, flatCategory});
        readEstateCategory.SaveAsync();
    }
}