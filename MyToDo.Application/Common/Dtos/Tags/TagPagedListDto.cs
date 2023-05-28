using MyToDo.Application.Common.Dtos.Common;

namespace MyToDo.Application.Common.Dtos.Tags;

public sealed class TagPagedListDto : BasePagedListDto<TagDto>
{
    public TagPagedListDto(List<TagDto> items, int totalCount, int pageIndex, int pageSize) 
        : base(items, totalCount, pageIndex, pageSize)
    {
    }

    public TagPagedListDto()
    {
    }
}
