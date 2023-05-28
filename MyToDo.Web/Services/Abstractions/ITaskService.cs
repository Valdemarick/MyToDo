using MyToDo.HttpContracts.Tasks;

namespace MyToDo.Web.Services.Abstractions;

internal interface ITaskService
{
    Task<TaskPagedListDto> GetPageAsync(TaskPageRequestDto dto, CancellationToken cancellationToken = default);
}
