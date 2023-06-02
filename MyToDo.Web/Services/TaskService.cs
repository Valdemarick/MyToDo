using MyToDo.Domain.Shared;
using MyToDo.HttpContracts.Tasks;
using MyToDo.Web.Extensions;
using MyToDo.Web.Services.Abstractions;

namespace MyToDo.Web.Services;

internal sealed class TaskService : BaseService, ITaskService
{
    private readonly HttpClient _httpClient;

    public TaskService(IHttpClientFactory httpClientFactory)
    {
        _httpClient = httpClientFactory.CreateClient("MyToDoServerClient");
    }

    protected override string BaseUrl => "tasks";

    public async Task<Result<TaskPagedListDto>> GetPageAsync(TaskPageRequestDto dto, CancellationToken cancellationToken = default)
    {
        var queryParameters = await dto.GetQueryFromRequestDtoAsync();
        var url = $"{BaseUrl}/page?{queryParameters}";

        var httpRequest = CreateHttpRequestMessage(HttpMethod.Get, url);

        using var response = await _httpClient.SendAsync(httpRequest, cancellationToken);

        return await HandleResponse<TaskPagedListDto>(response, cancellationToken);
    }

    public async Task<Result> CreateAsync(CreateTaskDto dto, CancellationToken cancellationToken = default)
    {
        var httpRequest = CreateHttpRequestMessage(HttpMethod.Post, BaseUrl, dto);

        using var response = await _httpClient.SendAsync(httpRequest, cancellationToken);

        return await HandleResponse(response, cancellationToken);
    }
}
