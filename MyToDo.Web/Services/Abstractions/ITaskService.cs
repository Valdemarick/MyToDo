using MyToDo.Application.Common.Dtos.Tasks;

namespace MyToDo.Web.Services.Abstractions;

internal interface ITaskService
{
    Task<List<PagedTaskDto>> GetAllTasksAsync(CancellationToken cancellationToken = default);
}
