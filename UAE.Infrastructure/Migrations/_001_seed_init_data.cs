using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Entities;
using UAE.Core.Entities;
using UAE.Core.EntityDataParameters;
using UAE.Core.EntityDataParameters.RealEstate;

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
            Fields = new Dictionary<string, object>()
            {
                {Field.Floor.ToString(), 2},
                {Field.NumberOfBedrooms.ToString(), 2},
                {Field.Number.ToString(), 23},
                {Field.BedroomType.ToString(), "shower"},
                {Field.YearOfBuilding.ToString(), 2012}
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
            Fields = new Dictionary<string, Dictionary<string, object>>
            {
                {
                    Field.Floor.ToString(), 
                    new Dictionary<string, object>
                    {
                        { FieldParameter.Type.ToString(), typeof(int).Name },
                        { FieldParameter.IsRequired.ToString(), true },
                    }
                },
                {
                    Field.NumberOfBedrooms.ToString(),
                    new Dictionary<string, object>
                    {
                        { FieldParameter.Type.ToString(), typeof(int).Name },
                        { FieldParameter.IsRequired.ToString(), true },
                    }
                },
                {
                    Field.Number.ToString(), 
                    new Dictionary<string, object>
                    {
                        { FieldParameter.Type.ToString(), typeof(int).Name },
                        { FieldParameter.IsRequired.ToString(), true },
                    }
                },
                {
                    Field.BedroomType.ToString(), 
                    new Dictionary<string, object>
                    {
                        { FieldParameter.Type.ToString(), typeof(string).Name },
                        { FieldParameter.MaxLength.ToString(), 20 },
                        { FieldParameter.MinLength.ToString(), 3 },
                        { FieldParameter.IsRequired.ToString(), true },
                    }
                },
                {
                    Field.YearOfBuilding.ToString(), 
                    new Dictionary<string, object>
                    {
                        { FieldParameter.Type.ToString(), typeof(int).Name },
                        { FieldParameter.IsRequired.ToString(), true },
                    }
                }
            }
        };

        var flatCategory = new Category
        {            
            ID = ObjectId.GenerateNewId().ToString(),
            Label = "flat",
            Fields = new Dictionary<string, Dictionary<string, object>>
            {
                {
                    Field.Floor.ToString(), 
                    new Dictionary<string, object>
                    {
                        { FieldParameter.Type.ToString(), typeof(int).Name },
                        { FieldParameter.IsRequired.ToString(), true },
                    }
                },
                {
                    Field.NumberOfBedrooms.ToString(), 
                    new Dictionary<string, object>
                    {
                        { FieldParameter.Type.ToString(), typeof(int).Name },
                        { FieldParameter.IsRequired.ToString(), true },
                    }
                },
                {
                    Field.Number.ToString(), 
                    new Dictionary<string, object>
                    {
                        { FieldParameter.Type.ToString(), typeof(int).Name },
                        { FieldParameter.IsRequired.ToString(), true },
                    }
                },
                {
                    Field.BedroomType.ToString(), 
                    new Dictionary<string, object>
                    {
                        { FieldParameter.Type.ToString(), typeof(string).Name },
                        { FieldParameter.MaxLength.ToString(), 20 },
                        { FieldParameter.MinLength.ToString(), 3 },
                        { FieldParameter.IsRequired.ToString(), true },
                    }
                },
                {
                    Field.YearOfBuilding.ToString(), 
                    new Dictionary<string, object>
                    {
                        { FieldParameter.Type.ToString(), typeof(int).Name },
                        { FieldParameter.IsRequired.ToString(), true },
                    }
                }
            }
        };

        var villaCategory = new Category
        {            
            ID = ObjectId.GenerateNewId().ToString(),
            Label = "villa",
            Fields =  new Dictionary<string, Dictionary<string, object>>
            {
                {
                    Field.NumberOfBedrooms.ToString(), 
                    new Dictionary<string, object>
                    {
                        { FieldParameter.Type.ToString(), typeof(int).Name },
                        { FieldParameter.IsRequired.ToString(), true },
                    }
                },
                {
                    Field.Number.ToString(), 
                    new Dictionary<string, object>
                    {
                        { FieldParameter.Type.ToString(), typeof(int).Name },
                        { FieldParameter.IsRequired.ToString(), true },
                    }
                },
                {
                    Field.BedroomType.ToString(), 
                    new Dictionary<string, object>
                    {
                        { FieldParameter.Type.ToString(), typeof(string).Name },
                        { FieldParameter.MaxLength.ToString(), 20 },
                        { FieldParameter.MinLength.ToString(), 3 },
                        { FieldParameter.IsRequired.ToString(), true },
                    }
                },
                {
                    Field.YearOfBuilding.ToString(), 
                    new Dictionary<string, object>
                    {
                        { FieldParameter.Type.ToString(), typeof(int).Name },
                        { FieldParameter.IsRequired.ToString(), true },
                    }
                }
            }
        };

        var carCategory = new Category
        {
            Label = "car",
            Fields = new Dictionary<string, Dictionary<string, object>>
            {
                {
                    Field.MaxSpeed.ToString(), 
                    new Dictionary<string, object>
                    {
                        { FieldParameter.Type.ToString(), typeof(int).Name },
                        { FieldParameter.IsRequired.ToString(), true },
                    }
                },
                {
                    Field.Mileage.ToString(), 
                    new Dictionary<string, object>
                    {
                        { FieldParameter.Type.ToString(), typeof(int).Name },
                        { FieldParameter.IsRequired.ToString(), true },
                    }
                },
                {
                    Field.Brand.ToString(), 
                    new Dictionary<string, object>
                    {
                        { FieldParameter.Type.ToString(), typeof(string).Name },
                        { FieldParameter.MaxLength.ToString(), 20 },
                        { FieldParameter.MinLength.ToString(), 3 },
                        { FieldParameter.IsRequired.ToString(), true },
                    }
                }
            }
        };

        var sportCar = new Category
        {          
            ID = ObjectId.GenerateNewId().ToString(),
            Label = "sport car",
            Fields = new Dictionary<string, Dictionary<string, object>>
            {
                {
                    Field.MaxSpeed.ToString(), 
                    new Dictionary<string, object>
                    {
                        { FieldParameter.Type.ToString(), typeof(int).Name },
                        { FieldParameter.IsRequired.ToString(), true },
                    }
                },
                {
                    Field.Mileage.ToString(), 
                    new Dictionary<string, object>
                    {
                        { FieldParameter.Type.ToString(), typeof(int).Name },
                        { FieldParameter.IsRequired.ToString(), true },
                    }
                },
                {
                    Field.Brand.ToString(), 
                    new Dictionary<string, object>
                    {
                        { FieldParameter.Type.ToString(), typeof(string).Name },
                        { FieldParameter.MaxLength.ToString(), 20 },
                        { FieldParameter.MinLength.ToString(), 3 },
                        { FieldParameter.IsRequired.ToString(), true },
                    }
                }
            }
        };


        carCategory.Children.Add(sportCar);
        await carCategory.SaveAsync();

        readEstateCategory.Children.AddRange(new[] {villaCategory, flatCategory});
        readEstateCategory.SaveAsync();
    }
}