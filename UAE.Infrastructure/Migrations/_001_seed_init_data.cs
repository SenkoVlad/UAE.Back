using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using MongoDB.Entities;
using UAE.Core.DataModels;
using UAE.Core.Entities;
using UAE.Core.EntityDataParameters;
using UAE.Core.EntityDataParameters.RealEstate;
using UAE.Shared.Extensions;

namespace UAE.Infrastructure.Data.Init;

public class _001_seed_init_data : IMigration
{
    public async Task UpgradeAsync()
    {
        await AddCurrenciesAsync();
        await AddCategoriesAsync();
        await AddUserAsync(); 
        await AddAnnouncementsAsync();
    }

    private async Task AddCurrenciesAsync()
    {
        await DB.DeleteAsync<Currency>(_ => true);

        var currencies = new List<Currency> { new Currency { Code = "USD" }, new Currency { Code = "AED" } };

        await currencies.SaveAsync();
    }

    private async Task AddUserAsync()
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

        var firstCurrency = await DB.Find<Currency>()
            .Match(s => s.Code == "USD")
            .ExecuteSingleAsync();
        
        var firstParentCategory = await DB.Find<Category>()
            .Match(s => s.Children.Any(c => c.Label == "Property For Rent"))
            .ExecuteSingleAsync();

        var firstCategory = firstParentCategory.Children.First(c => c.Label == "Property For Rent");
        
        var user = await DB.Find<User>()
            .Match(s => s.Email == "vlad@vlad.com")
            .ExecuteSingleAsync();
        
        var firstAnnouncement = new Announcement
        {
            Category = new One<Category>()
            {
                ID = firstCategory!.ID
            },
            CategoryPath = new CategoryPath[]
            {

                new CategoryPath
                {
                    ID = firstParentCategory.ID,
                    Label = firstParentCategory.Label
                },
                new CategoryPath
                {
                    ID = firstCategory.ID,
                    Label = firstCategory.Label
                }
            },
            Description = "flat 1",
            Fields = new BsonDocument
            {
                {ExtraFieldName.Floor.GetDescription(), 2},
                {ExtraFieldName.NumberOfBedrooms.GetDescription(), 2},
                {ExtraFieldName.Number.GetDescription(), 23},
                {ExtraFieldName.BathroomType.GetDescription(), "shower"},
                {ExtraFieldName.YearOfBuilding.GetDescription(), 2012}
            },
            Title = "flat super flat",
            User = new One<User>()
            {
                ID = user.ID
            },
            AddressToTake = "address to take",
            Address = "address",
            CreatedDateTime = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds(),
            Currency = new Currency
            {
                ID = firstCurrency.ID,
                Code = "USD"
            },
            Price = 100.40m
        };

        var secondCurrency = await DB.Find<Currency>()
            .Match(s => s.Code == "AED")
            .ExecuteSingleAsync();

        var secondParentCategory = await DB.Find<Category>()
            .Match(s => s.Children.Any(c => c.Label == "Property For Rent"))
            .ExecuteSingleAsync();

        var secondCategory = secondParentCategory.Children.First(c => c.Label == "Property For Rent");

        var secondAnnouncement = new Announcement
        {
            Category = new One<Category>()
            {
                ID = secondCategory!.ID
            },
            CategoryPath = new CategoryPath[]
            {

                new CategoryPath
                {
                    ID = secondParentCategory.ID,
                    Label = secondParentCategory.Label
                },
                new CategoryPath
                {
                    ID = secondCategory.ID,
                    Label = secondCategory.Label
                }
            },
            Description = "Maecenas vel sapien ac dui facilisis ultricies ac nec libero. Sed semper efficitur facilisis. Duis sed suscipit ante. Fusce quis rutrum nunc. Aliquam iaculis dolor ac est venenatis, id commodo sapien eleifend. Donec lectus massa, molestie vitae metus at, ullamcorper facilisis eros. Aliquam gravida congue neque, sit amet convallis nibh consectetur sed. In nisi nunc, mollis id sollicitudin ut, consectetur vel turpis. Cras nisl nibh, consectetur vitae arcu at, dapibus bibendum risus. Integer pulvinar neque ac purus egestas, non vestibulum erat tristique. Maecenas pulvinar eros eget lacus dignissim, in imperdiet ex condimentum. Quisque vulputate lacus a mi ultrices accumsan. Mauris sit amet velit ut metus sagittis mollis. Sed a gravida lacus, eu imperdiet odio. Nullam mattis sed diam quis ultrices. Morbi ultrices diam ut eros cursus, eu maximus sem faucibus. Donec cursus nisl quis dapibus facilisis. Integer faucibus eu risus consectetur malesuada. Donec ac gravida magna. Nunc tortor leo, bibendum id tristique et, scelerisque cursus lorem. Duis molestie leo purus, nec iaculis eros pellentesque et. Nullam faucibus lacus quis libero dictum pellentesque. Vestibulum eget fringilla massa. Praesent ac pulvinar erat. Nullam sapien metus, aliquet id aliquet tempor, euismod nec lectus. Proin at metus auctor, vestibulum nisl vitae, venenatis nibh. Cras fringilla justo non sagittis suscipit. Curabitur posuere, risus eget rhoncus imperdiet, dui justo feugiat quam, eu aliquam sapien leo vel orci. Class aptent taciti sociosqu ad litora torquent per conubia nostra, per inceptos himenaeos. Fusce hendrerit neque eros, sagittis imperdiet nulla consequat euismod. In tincidunt enim sed leo feugiat pellentesque. Pellentesque massa est, ultrices nec suscipit ut, semper ac urna. Quisque eu tortor enim. Proin fringilla diam ut feugiat tincidunt. Vestibulum convallis quam mattis augue euismod malesuada. Integer varius ligula lectus, in maximus dolor posuere eget. Sed dictum erat feugiat efficitur",
            Fields = new BsonDocument
            {
                {ExtraFieldName.Floor.GetDescription(), 2},
                {ExtraFieldName.NumberOfBedrooms.GetDescription(), 2},
                {ExtraFieldName.Number.GetDescription(), 23},
                {ExtraFieldName.BathroomType.GetDescription(), "shower"},
                {ExtraFieldName.YearOfBuilding.GetDescription(), 2012}
            },
            Title = "Quisque condimentum, elit vel vestibulum luctus, nisl arcu erat curae.",
            User = new One<User>()
            {
                ID = user.ID
            },
            AddressToTake = "address to take",
            Address = "address",
            CreatedDateTime = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds(),
            Currency = new Currency
            {
                ID = secondCurrency.ID,
                Code = "AED"
            },
            Price = 7654.32m
        };

        await new List<Announcement> { firstAnnouncement, secondAnnouncement }.SaveAsync();
    }

    private async Task AddCategoriesAsync()
    {
        await DB.DeleteAsync<Category>(_ => true);
        var categories = await GetAllcategoriesFromFileAsync();
        await categories.SaveAsync();
    }

    private async Task<List<Category>> GetAllcategoriesFromFileAsync()
    {
        var categories = new List<CategoryForMigration>();
        var parentCategoryLabels = new List<string>();
        var rootParentCategoryLabel = String.Empty;
        var rootParentCategoryName = String.Empty;
        await foreach (var line in System.IO.File.ReadLinesAsync(@"..\UAE.Infrastructure\Migrations\categories.txt"))
        {
            var inputRow = line.Split('@');
            
            if (inputRow.Length < 1)
            {
                return categories
                    .Select(c => c.ToEntity())
                    .ToList();
            }
            
            var currentCategories = inputRow.First().Split('/');

            if (currentCategories.Length > 3)
            {
                continue;
            }
            
            if (currentCategories.Length == 1)
            {
                rootParentCategoryName = currentCategories.First();
                rootParentCategoryLabel = inputRow.Last();
                parentCategoryLabels.Add(rootParentCategoryLabel);
            }

            FillCategoriesByInputRow(categories, inputRow, rootParentCategoryName);
        }

        parentCategoryLabels = parentCategoryLabels
            .Distinct()
            .ToList();

        var parentCategories = categories
            .Where(c => parentCategoryLabels.Contains(c.Label))
            .ToList();

        return parentCategories
            .Select(c => c.ToEntity())
            .ToList();
    }

    private void FillCategoriesByInputRow(List<CategoryForMigration> categories, string[] inputRow, string rootParentName)
    {
        var currentCategories = inputRow.First().Split('/');
        var currenctCategoryLabel = inputRow.Last();

        for (var index = 0; index < currentCategories.Length; index++)
        {
            var currentCategoryName = currentCategories[index];
            var isCategoryExist = categories.Any(c => string.Equals(c.Name, currentCategoryName, StringComparison.OrdinalIgnoreCase)
                                                      && c.RootParentName == rootParentName);
            
            if (isCategoryExist)
            {
                continue;
            }
            else
            {
                var category = new CategoryForMigration
                {
                    ID = ObjectId.GenerateNewId().ToString(),
                    Label = currenctCategoryLabel,
                    Name = currentCategoryName,
                    RootParentName = rootParentName
                };

                FillCategoryFields(category);

                if (index != 0)
                {
                    var currentSubParentCategory = GetCurrentSubParentCategory(categories, rootParentName, index, currentCategories);

                    if (!string.IsNullOrWhiteSpace(currentSubParentCategory?.ID))
                    {
                        currentSubParentCategory.Children.Add(category);
                    }
                }

                categories.Add(category);
            }
        }
    }

    private static CategoryForMigration? GetCurrentSubParentCategory(List<CategoryForMigration> categories, string rootParentName, int index,
        string[] currentCategories)
    {
        var currentSubParentCategory = new CategoryForMigration();

        if (currentCategories.Length > 1)
        {
            currentSubParentCategory = categories.FirstOrDefault(c =>
                string.Equals(c.Name, currentCategories[index - 1], StringComparison.OrdinalIgnoreCase)
                && c.RootParentName == rootParentName);
        }

        return currentSubParentCategory;
    }

    private static void FillCategoryFields(CategoryForMigration category)
    {
        switch (category.Label)
        {
            case "Cars & Vehicles":
                category.Fields = new List<Field>
                {
                    new Field(ExtraFieldName.Mileage.GetDescription(), typeof(int).Name),
                    new Field(ExtraFieldName.MaxSpeed.GetDescription(), typeof(int).Name),
                    new Field(ExtraFieldName.Brand.GetDescription(), typeof(string).Name),
                    new Field(ExtraFieldName.YearOfBuilding.GetDescription(), typeof(int).Name),
                    new Field(ExtraFieldName.BathroomType.GetDescription(), typeof(int).Name),
                };
                break;
            case "Property For Rent":
                category.Fields = new List<Field>
                {
                    new Field(ExtraFieldName.Floor.GetDescription(), typeof(int).Name),
                    new Field(ExtraFieldName.NumberOfBedrooms.GetDescription(), typeof(int).Name),
                    new Field(ExtraFieldName.Number.GetDescription(), typeof(int).Name),
                    new Field(ExtraFieldName.YearOfBuilding.GetDescription(), typeof(int).Name),
                    new Field(ExtraFieldName.BathroomType.GetDescription(), typeof(string).Name)
                };
                break;
        }
    }
}

internal class CategoryForMigration
{
    public string ID { get; set; }
    public string Label { get; set; }

    public string Name { get; set; }

    public string RootParentName { get; set; }
    
    public List<CategoryForMigration> Children { get; set; } = new();

    public List<Field> Fields { get; set; } = new();

    public Category ToEntity()
    {
        return new Category()
        {
            ID = ID,
            Label = Label,
            Children = Children
                .Select(ch => ch.ToEntity())
                .ToList(),
            Fields = Fields,
            Name = Name
        };
    }
}