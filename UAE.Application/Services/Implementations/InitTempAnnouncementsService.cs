using Faker;
using MongoDB.Bson;
using MongoDB.Entities;
using UAE.Application.Services.Interfaces;
using UAE.Application.Services.Interfaces.Base;
using UAE.Core.Entities;
using UAE.Core.Repositories;
using UAE.Shared.Enum;

namespace UAE.Application.Services.Implementations;

public class InitTempAnnouncementsService
{
    private readonly ICategoryInMemory _categoryInMemory;
    private readonly IInMemoryService<Core.DataModels.Currency> _currencyInMemory;
    private readonly IUserRepository _userRepository;
    private readonly IAnnouncementRepository _announcementRepository;

    public InitTempAnnouncementsService(ICategoryInMemory categoryInMemory, 
        IInMemoryService<Core.DataModels.Currency> currencyInMemory,
        IUserRepository userRepository,
        IAnnouncementRepository announcementRepository)
    {
        _categoryInMemory = categoryInMemory;
        _currencyInMemory = currencyInMemory;
        _userRepository = userRepository;
        _announcementRepository = announcementRepository;
    }

    public async Task InitAsync()
    {
        var announcementCount = (await _announcementRepository.GetAllAsync()).Count;

        if (announcementCount > 100)
        {
            return;
        }
        
        var announcements = new List<Announcement>();
        var categories = _categoryInMemory.CategoryWithParentPathModels;
        var user = await _userRepository.GetByEmailAsync("vlad@vlad.com");

        foreach (var category in categories)
        {
            var fields = CreateFields(category.Fields);
            var categoryPath = _categoryInMemory.GetCategoryPath(category.Category.ID);

            for (int i = 0; i < 10; i++)
            {
                var announcement = new Announcement
                {
                    ID = ObjectId.GenerateNewId().ToString(),
                    Address = string.Concat(Faker.Address.City(), ", ", Faker.Address.StreetName(), ", ",
                        Faker.Address.Country()),
                    Category = category.Category.ID,
                    Description = string.Join(", ", Faker.Lorem.Sentences(2)),
                    Fields = fields,
                    Currency = _currencyInMemory.Data.First(),
                    Price = (decimal) (Faker.RandomNumber.Next(1, 100000) / 100.0),
                    Title = string.Join(" ", Faker.Lorem.Words(10)),
                    User = user!.ID,
                    CategoryPath = categoryPath,
                    AddressToTake = string.Concat(Faker.Address.City(), ", ", Faker.Address.StreetName(), ", ",
                        Faker.Address.Country()),
                    CreatedDateTime = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds(),
                };

                announcements.Add(announcement);
            }
        }

        await announcements.SaveAsync();
    }

    private BsonDocument CreateFields(List<Field> categoryFields)
    {
        var fields = new BsonDocument();
        
        foreach (var categoryField in categoryFields)
        {
            var value = CreateValue(categoryField);
            fields.Add(new BsonElement(categoryField.Name, value));
        }

        return fields;
    }

    private BsonValue CreateValue(Field categoryField)
    {
        switch (categoryField.ValueType)
        {
            case FieldValueType.Int32:
                return new BsonInt32(Faker.RandomNumber.Next(1, 10000));
            case FieldValueType.Decimal:
                var value = Faker.RandomNumber.Next(1, 1000000) / 100.0;
                return new BsonDecimal128((decimal)value);
            case FieldValueType.String:
                return new BsonString(Faker.Lorem.Words(1).First());
            default:
                return new BsonString(Faker.Lorem.Words(1).First());
        }
    }
}