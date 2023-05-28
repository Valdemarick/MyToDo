using MyToDo.HttpContracts.Common;

namespace MyToDo.HttpContracts.Tasks;

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
