using MyToDo.Application.Common.Dtos.Tasks;
using MyToDo.Web.Services.Abstractions;

namespace MyToDo.Web.Services;

internal sealed class TaskService : ITaskService
{
    private readonly IHttpClientFactory _httpClientFactory;

    private readonly HttpClient _client;

    private const string _baseUrl = "tasks";

    public TaskService(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;

        _client = _httpClientFactory.CreateClient("MyToDoServerClient");
    }

    public async Task<List<PagedTaskDto>> GetAllTasksAsync(CancellationToken cancellationToken = default)
    {
        var getTasksResult = await _client.GetFromJsonAsync<List<PagedTaskDto>>($"{_baseUrl}/page",cancellationToken);
        
        return getTasksResult ?? new List<PagedTaskDto>();
    }
}
