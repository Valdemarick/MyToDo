using MyToDo.Domain.Errors;
using MyToDo.Domain.Shared;
using MyToDo.HttpContracts.Tags;
using MyToDo.Web.Extensions;
using MyToDo.Web.Services.Abstractions;

namespace MyToDo.Web.Services;

internal sealed class TagService : BaseService, ITagService
{
    private readonly HttpClient _client;

    public TagService(IHttpClientFactory httpClientFactory)
    {
        _client = httpClientFactory.CreateClient("MyToDoServerClient");
    }

    protected override string BaseUrl => "tags";
    
    public async Task<Result<TagPagedListDto>> GetPageAsync(TagPageRequestDto dto, CancellationToken cancellationToken = default)
    {
        var queryParameters = await dto.GetQueryFromRequestDtoAsync();
        var url = $"{BaseUrl}/page?{queryParameters}";

        var httpRequest = CreateHttpRequestMessage(HttpMethod.Get, url);

        using var response = await _client.SendAsync(httpRequest, cancellationToken);
        if (!response.IsSuccessStatusCode)
        {
            return await HandleError(response, cancellationToken);
        }

        var tags = await response.Content.ReadFromJsonAsync<TagPagedListDto>(cancellationToken: cancellationToken);

        return tags is not null ? Result.Success(tags) : Result.Failure(DomainErrors.FailedToDeserializeObject);
    }

    public async Task<Result> CreateAsync(CreateTagDto dto, CancellationToken cancellationToken = default)
    {
        var httpRequest = CreateHttpRequestMessage(HttpMethod.Post, BaseUrl, dto);

        using var response = await _client.SendAsync(httpRequest, cancellationToken);
        if (response.IsSuccessStatusCode)
        {
            return Result.Success();
        }

        return await HandleError(response, cancellationToken);
    }

    public async Task<Result> UpdateAsync(UpdateTagDto dto, CancellationToken cancellationToken = default)
    {
        var httpRequest = CreateHttpRequestMessage(HttpMethod.Put, BaseUrl, dto);

        using var response = await _client.SendAsync(httpRequest, cancellationToken);
        if (response.IsSuccessStatusCode)
        {
            return Result.Success();
        }

        return await HandleError(response, cancellationToken);
    }

    public async Task<Result> DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var url = $"{BaseUrl}/{id}";

        var httpRequest = CreateHttpRequestMessage(HttpMethod.Delete, url);

        using var response = await _client.SendAsync(httpRequest, cancellationToken);
        if (response.IsSuccessStatusCode)
        {
            return Result.Success();
        }

        return await HandleError(response, cancellationToken);
    }
}
