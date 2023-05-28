using MyToDo.Application.Common.Dtos.Common;

namespace MyToDo.Application.Common.Dtos.Tasks;

public sealed class TaskPagedListDto : BasePagedListDto<TaskShortInfoDto>
{
    public TaskPagedListDto(List<TaskShortInfoDto> items, int totalCount, int pageIndex, int pageSize) 
        : base(items, totalCount, pageIndex, pageSize)
    {
    }
    
    public TaskPagedListDto()
    {
    }
}
