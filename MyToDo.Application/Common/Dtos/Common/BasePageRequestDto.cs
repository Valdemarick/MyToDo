namespace MyToDo.Application.Common.Dtos.Common;

public abstract class BasePageRequestDto
{
    public string? SearchString { get; set; }

    public int PageIndex { get; set; }

    public int PageSize { get; set; }
}
