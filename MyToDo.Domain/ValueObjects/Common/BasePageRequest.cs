namespace MyToDo.Domain.ValueObjects.Common;

public abstract class BasePageRequest
{
    public string? SearchString { get; init; }

    public required int PageIndex { get; init; }

    public required int PageSize { get; init; }
}