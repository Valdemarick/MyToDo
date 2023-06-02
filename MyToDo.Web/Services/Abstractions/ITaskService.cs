using MyToDo.Domain.Shared;
using MyToDo.HttpContracts.Tasks;

namespace MyToDo.Web.Services.Abstractions;

internal interface ITaskService
{
    Task<Result<TaskPagedListDto>> GetPageAsync(TaskPageRequestDto dto, CancellationToken cancellationToken = default);

    Task<Result> CreateAsync(CreateTaskDto dto, CancellationToken cancellationToken = default);
}
