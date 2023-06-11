using System.Net.Http.Headers;
using Microsoft.AspNetCore.Components.Authorization;
using MyToDo.Domain.Errors;
using MyToDo.Domain.Shared;
using MyToDo.HttpContracts.Tags;
using MyToDo.Web.Extensions;
using MyToDo.Web.Services.Abstractions;

namespace MyToDo.Web.Services;

internal sealed class TagService : BaseService, ITagService
{
    public TagService(IHttpClientFactory httpClientFactory,
        AuthenticationStateProvider authenticationStateProvider) : base(httpClientFactory, authenticationStateProvider)
    {
    }

    protected override string BaseUrl => "tags";

    public async Task<Result<List<TagDto>>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        var httpRequest = await CreateHttpRequestMessage(HttpMethod.Get, BaseUrl);

        using var response = await HttpClient.SendAsync(httpRequest, cancellationToken);

        return await HandleResponse<List<TagDto>>(response, cancellationToken);
    }

    public async Task<Result<TagPagedListDto>> GetPageAsync(TagPageRequestDto dto, CancellationToken cancellationToken = default)
    {
        var queryParameters = await dto.GetQueryFromRequestDtoAsync();
        var url = $"{BaseUrl}/page?{queryParameters}";

        var httpRequest = await CreateHttpRequestMessage(HttpMethod.Get, url);

        using var response = await HttpClient.SendAsync(httpRequest, cancellationToken);

        return await HandleResponse<TagPagedListDto>(response, cancellationToken);
    }

    public async Task<Result> CreateAsync(CreateTagDto dto, CancellationToken cancellationToken = default)
    {
        var httpRequest = await CreateHttpRequestMessage(HttpMethod.Post, BaseUrl, dto);

        using var response = await HttpClient.SendAsync(httpRequest, cancellationToken);
        if (response.IsSuccessStatusCode)
        {
            return Result.Success();
        }

        return await HandleError(response, cancellationToken);
    }

    public async Task<Result> UpdateAsync(UpdateTagDto dto, CancellationToken cancellationToken = default)
    {
        var httpRequest = await CreateHttpRequestMessage(HttpMethod.Put, BaseUrl, dto);

        using var response = await HttpClient.SendAsync(httpRequest, cancellationToken);
        if (response.IsSuccessStatusCode)
        {
            return Result.Success();
        }

        return await HandleError(response, cancellationToken);
    }

    public async Task<Result> DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var url = $"{BaseUrl}/{id}";

        var httpRequest = await CreateHttpRequestMessage(HttpMethod.Delete, url);

        using var response = await HttpClient.SendAsync(httpRequest, cancellationToken);
        if (response.IsSuccessStatusCode)
        {
            return Result.Success();
        }

        return await HandleError(response, cancellationToken);
    }
}
