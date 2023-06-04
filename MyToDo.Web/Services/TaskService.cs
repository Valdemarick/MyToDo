using MyToDo.Domain.Shared;
using MyToDo.HttpContracts.Tasks;
using MyToDo.Web.Extensions;
using MyToDo.Web.Services.Abstractions;

namespace MyToDo.Web.Services;

internal sealed class TaskService : BaseService, ITaskService
{
    public TaskService(IHttpClientFactory httpClientFactory) : base(httpClientFactory)
    {
    }

    protected override string BaseUrl => "tasks";

    public async Task<Result<TaskPagedListDto>> GetPageAsync(TaskPageRequestDto dto, CancellationToken cancellationToken = default)
    {
        var queryParameters = await dto.GetQueryFromRequestDtoAsync();
        var url = $"{BaseUrl}/page?{queryParameters}";

        var httpRequest = CreateHttpRequestMessage(HttpMethod.Get, url);

        using var response = await HttpClient.SendAsync(httpRequest, cancellationToken);

        return await HandleResponse<TaskPagedListDto>(response, cancellationToken);
    }

    public async Task<Result<TaskFullInfoDto>> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var url = $"{BaseUrl}/{id}";

        var httpRequest = CreateHttpRequestMessage(HttpMethod.Get, url);

        using var response = await HttpClient.SendAsync(httpRequest, cancellationToken);

        return await HandleResponse<TaskFullInfoDto>(response, cancellationToken);
    }

    public async Task<Result> CreateAsync(CreateTaskDto dto, CancellationToken cancellationToken = default)
    {
        var httpRequest = CreateHttpRequestMessage(HttpMethod.Post, BaseUrl, dto);

        using var response = await HttpClient.SendAsync(httpRequest, cancellationToken);

        return await HandleResponse(response, cancellationToken);
    }

    public async Task<Result> UpdateAsync(UpdateTaskDto dto, CancellationToken cancellationToken = default)
    {
        var httpRequest = CreateHttpRequestMessage(HttpMethod.Put, BaseUrl, dto);

        using var response = await HttpClient.SendAsync(httpRequest, cancellationToken);

        return await HandleResponse(response, cancellationToken);
    }
}
