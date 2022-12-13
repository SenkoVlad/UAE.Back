using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Entities;
using UAE.Core.Entities;
using UAE.Core.EntityDataParameters;
using UAE.Core.EntityDataParameters.RealEstate;
using UAE.Shared.Extensions;

namespace UAE.Infrastructure.Data.Init;

public class _001_seed_init_data : IMigration
{
    public async Task UpgradeAsync()
    {
        await AddCategoriesAsync();
        await AddUser(); 
        await AddAnnouncementsAsync();
    }

    private async Task AddUser()
    {
        await DB.DeleteAsync<User>(_ => true);

        var user = new User
        {
            Email = "vlad@vlad.com"
        };

        await user.SaveAsync();
    }

    private async Task AddAnnouncementsAsync()
    {
        await DB.DeleteAsync<Announcement>(_ => true);

        var category = await DB.Find<Category>()
            .Match(s => s.Label == "real estate")
            .ExecuteSingleAsync();

        var user = await DB.Find<User>()
            .Match(s => s.Email == "vlad@vlad.com")
            .ExecuteSingleAsync();
        
        var announcement = new Announcement
        {
            Category = new One<Category>()
            {
                ID = category.ID
            },
            Description = "flat 1",
            Fields = new BsonDocument
            {
                {ExtraFieldName.Floor.GetDescription(), 2},
                {ExtraFieldName.NumberOfBedrooms.GetDescription(), 2},
                {ExtraFieldName.Number.GetDescription(), 23},
                {ExtraFieldName.BedroomType.GetDescription(), "shower"},
                {ExtraFieldName.YearOfBuilding.GetDescription(), 2012}
            },
            Title = "flat super flat",
            User = new One<User>()
            {
                ID = user.ID
            },
            CreatedDateTime = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds()
        };

        await announcement.SaveAsync();
    }

    private static async Task AddCategoriesAsync()
    {
        await DB.DeleteAsync<Category>(_ => true);

        var readEstateCategory = new Category
        {
            Label = "real estate",
            Fields = new List<Field>
            {
                new Field(ExtraFieldName.Floor.GetDescription(), typeof(int).Name),
                new Field(ExtraFieldName.NumberOfBedrooms.GetDescription(), typeof(int).Name),
                new Field(ExtraFieldName.Number.GetDescription(), typeof(int).Name),
                new Field(ExtraFieldName.YearOfBuilding.GetDescription(), typeof(int).Name),
                new Field(ExtraFieldName.BedroomType.GetDescription(), typeof(int).Name)
            }
        };

        var flatCategory = new Category
        {            
            ID = ObjectId.GenerateNewId().ToString(),
            Label = "flat",
            Fields = new List<Field>
            {
                new Field(ExtraFieldName.Floor.GetDescription(), typeof(int).Name),
                new Field(ExtraFieldName.NumberOfBedrooms.GetDescription(), typeof(int).Name),
                new Field(ExtraFieldName.Number.GetDescription(), typeof(int).Name),
                new Field(ExtraFieldName.YearOfBuilding.GetDescription(), typeof(int).Name),
            }
        };

        var villaCategory = new Category
        {            
            ID = ObjectId.GenerateNewId().ToString(),
            Label = "villa",
            Fields = new List<Field>
            {
                new Field(ExtraFieldName.Floor.GetDescription(), typeof(int).Name),
                new Field(ExtraFieldName.NumberOfBedrooms.GetDescription(), typeof(int).Name),
                new Field(ExtraFieldName.Number.GetDescription(), typeof(int).Name),
                new Field(ExtraFieldName.YearOfBuilding.GetDescription(), typeof(int).Name),
                new Field(ExtraFieldName.BedroomType.GetDescription(), typeof(int).Name),
            }
        };

        var carCategory = new Category
        {
            Label = "car",
            Fields = new List<Field>
            {
                new Field(ExtraFieldName.Mileage.GetDescription(), typeof(int).Name),
                new Field(ExtraFieldName.MaxSpeed.GetDescription(), typeof(int).Name),
                new Field(ExtraFieldName.Brand.GetDescription(), typeof(int).Name),
                new Field(ExtraFieldName.YearOfBuilding.GetDescription(), typeof(int).Name),
                new Field(ExtraFieldName.BedroomType.GetDescription(), typeof(int).Name),
            }
        };

        var sportCar = new Category
        {          
            ID = ObjectId.GenerateNewId().ToString(),
            Label = "sport car",
            Fields = new List<Field>
            {
                new Field(ExtraFieldName.Mileage.GetDescription(), typeof(int).Name),
                new Field(ExtraFieldName.MaxSpeed.GetDescription(), typeof(int).Name),
                new Field(ExtraFieldName.Brand.GetDescription(), typeof(int).Name)
            }
        };

        carCategory.Children.Add(sportCar);
        await carCategory.SaveAsync();

        readEstateCategory.Children.AddRange(new[] {villaCategory, flatCategory});
        readEstateCategory.SaveAsync();
    }
}