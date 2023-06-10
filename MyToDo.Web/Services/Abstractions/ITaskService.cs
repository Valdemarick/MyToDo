using MyToDo.Domain.Shared;
using MyToDo.HttpContracts.Tags;
using MyToDo.HttpContracts.Tasks;

namespace MyToDo.Web.Services.Abstractions;

internal interface ITaskService
{
    Task<Result<TaskPagedListDto>> GetPageAsync(TaskPageRequestDto dto, CancellationToken cancellationToken = default);
    Task<Result<TaskFullInfoDto>> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<Result> CreateAsync(CreateTaskDto dto, CancellationToken cancellationToken = default);
    Task<Result> UpdateAsync(UpdateTaskDto dto, CancellationToken cancellationToken = default);
    Task<Result> LinkTagsToTaskAsync(LinkTagsToTaskDto dto, CancellationToken cancellationToken = default);
    Task<Result<List<TagDto>>> GetLinkedTagsAsync(Guid taskId, CancellationToken cancellationToken = default);
    Task<Result> CompleteAsync(Guid id, CancellationToken cancellationToken = default);
}
