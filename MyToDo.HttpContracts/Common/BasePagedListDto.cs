namespace MyToDo.HttpContracts.Common;

public abstract class BasePagedListDto<TDto>
{
    protected BasePagedListDto(List<TDto> items, int totalCount, int pageIndex, int pageSize)
    {
        Items = items;

        PageView = new PageViewDto
        {
            PageSize = pageSize,
            CurrentPage = pageIndex,
            TotalCount = totalCount,
            TotalPages = (int)Math.Ceiling((double)totalCount /pageSize)
        };
    }

    public BasePagedListDto()
    {
    }

    public List<TDto> Items { get; set; }

    public PageViewDto PageView { get; set; }   
}
