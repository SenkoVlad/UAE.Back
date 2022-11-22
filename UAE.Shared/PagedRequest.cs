namespace UAE.Shared;

public class PagedRequest
{
    public int PageNumber { get; set; }
        
    public int PageSize { get; set; }

    public string SortedBy { get; set; }
}