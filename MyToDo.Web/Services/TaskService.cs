using MyToDo.HttpContracts.Tasks;
using MyToDo.Web.Extensions;
using MyToDo.Web.Services.Abstractions;

namespace MyToDo.Web.Services;

internal sealed class TaskService : ITaskService
{
    private readonly HttpClient _client;

    private const string BaseUrl = "tasks";

    public TaskService(IHttpClientFactory httpClientFactory)
    {
        _client = httpClientFactory.CreateClient("MyToDoServerClient");
    }

    public async Task<TaskPagedListDto> GetPageAsync(TaskPageRequestDto dto, CancellationToken cancellationToken = default)
    {
        var queryParameters = dto.GetQueryFromRequestDto();
        var url = $"{BaseUrl}/page?{queryParameters}";
        
        var getTasksResult = await _client.GetFromJsonAsync<TaskPagedListDto>(url, cancellationToken);

        return getTasksResult;
    }
}
