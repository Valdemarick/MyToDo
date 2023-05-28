using MyToDo.Application.Common.Dtos.Tasks;
using MyToDo.Web.Extensions;
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

    public async Task<TaskPagedListDto> GetPageAsync(TaskPageRequestDto dto, CancellationToken cancellationToken = default)
    {
        var queryParameters = dto.GetQueryFromRequestDto();
        var url = $"{_baseUrl}/page?{queryParameters}";
        
        var getTasksResult = await _client.GetFromJsonAsync<TaskPagedListDto>(url, cancellationToken);

        return getTasksResult;
    }
}
