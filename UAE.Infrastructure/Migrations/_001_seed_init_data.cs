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

        var currency = new Currency
        {
            Code = "USD"
        };

        await currency.SaveAsync();
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

        var currency = await DB.Find<Currency>()
            .Match(s => s.Code == "USD")
            .ExecuteSingleAsync();
        
        var parentCategory = await DB.Find<Category>()
            .Match(s => s.Children.Any(c => c.Label == "Property For Rent"))
            .ExecuteSingleAsync();

        var category = parentCategory.Children.First(c => c.Label == "Property For Rent");
        
        var user = await DB.Find<User>()
            .Match(s => s.Email == "vlad@vlad.com")
            .ExecuteSingleAsync();
        
        var announcement = new Announcement
        {
            Category = new One<Category>()
            {
                ID = category!.ID
            },
            CategoryPath = new CategoryPath[]
            {

                new CategoryPath
                {
                    ID = parentCategory.ID,
                    Label = parentCategory.Label
                },
                new CategoryPath
                {
                    ID = category.ID,
                    Label = category.Label
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
                ID = currency.ID,
                Code = "USD"
            },
            Price = 100.40m
        };

        await announcement.SaveAsync();
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
            // var currentSubParentCategoryName = currentCategories.First();
            
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