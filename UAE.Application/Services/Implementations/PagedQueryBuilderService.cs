using MongoDB.Bson;
using MongoDB.Entities;
using UAE.Application.Models.Announcement;
using UAE.Application.Services.Interfaces;
using UAE.Core.Entities;
using UAE.Shared.Enum;

namespace UAE.Application.Services.Implementations;

internal class PagedQueryBuilderService<T> : IPagedQueryBuilderService<T> where T : Announcement, IEntity
{
    private readonly PagedSearch<T> _query;
    
    public PagedSearch<T> GetQuery() => _query;

    public PagedQueryBuilderService()
    {
        _query = DB.PagedSearch<T>();
    }

    public void BuildSearchQuery(SearchAnnouncementModel searchAnnouncementModel)
    {
        if (!string.IsNullOrWhiteSpace(searchAnnouncementModel.CategoryId))
        {
            _query.Match(a => a.Category.ID == searchAnnouncementModel.CategoryId);
        }

        foreach (var field in searchAnnouncementModel.Filters)
        {
            BuildSearchQueryForField(field);
        }

        if (searchAnnouncementModel.Description != null)
        {
            BuildSearchQueryForStringField(searchAnnouncementModel);
        }

        if (searchAnnouncementModel.Price != null)
        {
            BuildSearchQueryForDoubleField(searchAnnouncementModel);
        }

        _query.Sort(a => a.Ascending(searchAnnouncementModel.SortedBy));

        _query.PageSize(searchAnnouncementModel.PageSize)
            .PageNumber(searchAnnouncementModel.PageNumber);
    }

    private void BuildSearchQueryForDoubleField(SearchAnnouncementModel searchAnnouncementModel)
    {
        switch (searchAnnouncementModel.Price!.FilterCriteria)
        {
            case FilterCriteria.Equals:
                _query.Match( a=> a.Price.Equals(searchAnnouncementModel.Price!.FieldValue));
                break;
            case FilterCriteria.MoreAndEquals:
                _query.Match( a=> a.Price >= searchAnnouncementModel.Price!.FieldValue);
                break;
            case FilterCriteria.LessAndEquals:
                _query.Match( a=> a.Price <= searchAnnouncementModel.Price!.FieldValue);
                break;
            case FilterCriteria.More:
                _query.Match( a=> a.Price > searchAnnouncementModel.Price!.FieldValue);
                break;
            case FilterCriteria.Less:
                _query.Match( a=> a.Price < searchAnnouncementModel.Price!.FieldValue);
                break;
            default:
                _query.Match( a=> a.Description.Equals(searchAnnouncementModel.Description.FieldValue));
                break;
        }
    }

    private void BuildSearchQueryForField(BsonElement searchAnnouncementModel)
    {
        (BsonValue? value, FilterCriteria? criteria) = GetValueAndCriteria(searchAnnouncementModel);

        if (value == null || criteria == null)
        {
            return;
        }
        
        switch (criteria)
        {
            case FilterCriteria.More:
                _query.Match(a => a.Fields[searchAnnouncementModel.Name] > value);
                break;
            case FilterCriteria.Less:
                _query.Match(a => a.Fields[searchAnnouncementModel.Name] < value);
                break;
            case FilterCriteria.LessAndEquals:
                _query.Match(a => a.Fields[searchAnnouncementModel.Name] <= value);
                break;
            case FilterCriteria.MoreAndEquals:
                _query.Match(a => a.Fields[searchAnnouncementModel.Name] >= value);
                break;
            case FilterCriteria.Contains:
                _query.Match(a => a.Fields[searchAnnouncementModel.Name].AsString.Contains(value.AsString));
                break;
        }
    }

    private static (BsonValue? value, FilterCriteria? criteria) GetValueAndCriteria(BsonElement searchAnnouncementModel)
    {
        var filter = searchAnnouncementModel.Value.ToBsonDocument();
        var filterValueName = filter.Names.FirstOrDefault();
        var filterCriteriaName = filter.Names.LastOrDefault();

        if (filterCriteriaName == null || filterValueName == null)
        {
            return (null, null);
        }
        
        var value = filter[filterValueName].AsBsonValue;
        var criteria = (FilterCriteria) filter[filterCriteriaName].ToInt32();

        return (value, criteria);
    }

    private void BuildSearchQueryForStringField(SearchAnnouncementModel searchAnnouncementModel)
    {
        switch (searchAnnouncementModel.Description!.FilterCriteria)
        {
            case FilterCriteria.Equals:
                _query.Match( a=> a.Description.Equals(searchAnnouncementModel.Description.FieldValue));
                break;
            case FilterCriteria.Contains:
                _query.Match( a=> a.Description.Contains(searchAnnouncementModel.Description.FieldValue));
                break;
            default:
                _query.Match( a=> a.Description.Equals(searchAnnouncementModel.Description.FieldValue));
                break;
        }
    }
}