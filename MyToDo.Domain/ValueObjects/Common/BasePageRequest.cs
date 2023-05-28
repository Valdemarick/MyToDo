namespace MyToDo.Domain.ValueObjects.Common;

public abstract class BasePageRequest
{
    protected BasePageRequest(string? searchString, int pageIndex, int pageSize)
    {
        SearchString = searchString;
        PageIndex = pageIndex;
        PageSize = pageSize;
    }
    
    public string? SearchString { get; private set; }

    public int PageIndex { get; private set; }

    public int PageSize { get; private set; }
}
