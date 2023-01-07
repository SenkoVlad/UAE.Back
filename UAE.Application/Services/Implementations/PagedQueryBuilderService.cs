using MongoDB.Bson;
using MongoDB.Entities;
using UAE.Application.Extensions;
using UAE.Application.Models.Announcement;
using UAE.Application.Services.Interfaces;
using UAE.Core.Entities;
using UAE.Shared.Enum;

namespace UAE.Application.Services.Implementations;

internal class PagedQueryBuilderService<T> : IPagedQueryBuilderService<T> where T : Announcement, IEntity
{
    private readonly PagedSearch<T> _query;
    private readonly ICategoryInMemory _categoryInMemory;
    
    public PagedSearch<T> GetQuery() => _query;

    public PagedQueryBuilderService(ICategoryInMemory categoryInMemory)
    {
        _categoryInMemory = categoryInMemory;
        _query = DB.PagedSearch<T>();
    }

    public void BuildSearchQuery(SearchAnnouncementModel searchAnnouncementModel)
    {
        if (searchAnnouncementModel.CategoryIds != null && searchAnnouncementModel.CategoryIds.Any())
        {
            var categoryIds = searchAnnouncementModel.CategoryIds.Distinct();
            _query.Match(a => categoryIds.Contains(a.Category.ID));
            BuildSearchQueryForField(searchAnnouncementModel);
        }

        if (!string.IsNullOrWhiteSpace(searchAnnouncementModel.Description))
        {
            _query.Match(a => a.Description.Contains(searchAnnouncementModel.Description));
        }

        if (searchAnnouncementModel.Price != null)
        {
            BuildQueryForPriceField(searchAnnouncementModel.Price);
        }

        _query.Sort(a => a.Ascending(searchAnnouncementModel.SortedBy));
        _query.PageSize(searchAnnouncementModel.PageSize)
            .PageNumber(searchAnnouncementModel.PageNumber);
    }

    private void BuildQueryForPriceField(decimal?[] prices)
    {
        var priceFrom = prices[0];
        var priceTo = prices[1];
        
        if (priceFrom != null)
        {
            _query.Match( a=> a.Price >= priceFrom);
        }

        if (priceTo != null)
        {
            _query.Match( a=> a.Price <= priceTo);
        }
    }

    private void BuildSearchQueryForField(SearchAnnouncementModel searchAnnouncementModel)
    {
        if (searchAnnouncementModel.Filters == null)
        {
            return;
        }
        
        foreach (var fieldName in searchAnnouncementModel.Filters.Keys)
        {
            var searchCategoryId = searchAnnouncementModel.CategoryIds!.First();
            var categoryField = _categoryInMemory.CategoryWithParentPathModels
                .FirstOrDefault(c => c.Category.Fields.Any(f => f.Name == fieldName)
                                     && c.Category.ID == searchCategoryId);

            var field = categoryField?.Fields.FirstOrDefault(f => f.Name == fieldName);

            if (field == null)
            {
                continue;
            }

            var filterFieldValues = searchAnnouncementModel.Filters[fieldName]
                .Select(c => c.ToBsonValueByTypeName(field.ValueType))
                .ToArray();
            switch (field.Criteria)
            {
                case FilterCriteria.Contains:
                    _query.Match(a => filterFieldValues.Contains(a.Fields[fieldName]));
                    break;
                case FilterCriteria.Equals:
                    _query.Match(a => a.Fields[fieldName] == filterFieldValues[0]);
                    break;
                case FilterCriteria.InRange:
                    var fromValue = filterFieldValues[0];
                    var toValue = filterFieldValues[1];

                    if (fromValue != null)
                    {
                        _query.Match(a => a.Fields[fieldName] >= fromValue);
                    }

                    if (toValue != null)
                    {
                        _query.Match(a => a.Fields[fieldName] <= toValue);
                    }
                    break;
            }
        }
    }
}