using System.Text;
using MyToDo.Domain.Errors;
using MyToDo.Domain.Shared;
using MyToDo.HttpContracts.Members;
using MyToDo.Web.Extensions;
using MyToDo.Web.Services.Abstractions;
using Newtonsoft.Json;

namespace MyToDo.Web.Services;

internal sealed class MemberService : IMemberService
{
    private readonly HttpClient _client;
    
    private const string BaseUrl = "members";

    public MemberService(IHttpClientFactory httpClientFactory)
    {
        _client = httpClientFactory.CreateClient("MyToDoServerClient");
    }

    public async Task<Result<MemberDto>> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var url = $"{BaseUrl}/{id}";

        var httpRequest = new HttpRequestMessage(HttpMethod.Get, url);

        using var response = await _client.SendAsync(httpRequest, cancellationToken);
        if (!response.IsSuccessStatusCode)
        {
            var error = await response.Content.ReadFromJsonAsync<Error>(cancellationToken: cancellationToken);

            return Result.Failure(error ?? DomainErrors.FailedToDeserializeObject);
        }

        var memberDto = await response.Content.ReadFromJsonAsync<MemberDto>(cancellationToken: cancellationToken);
        
        return memberDto is not null ? Result.Success(memberDto) : Result.Failure(DomainErrors.FailedToDeserializeObject);
    }

    public async Task<Result<MemberPagedListDto>> GetPageAsync(MemberPageRequestDto dto, CancellationToken cancellationToken = default)
    {
        var queryParameters = await dto.GetQueryFromRequestDto();
        var url = $"{BaseUrl}/page?{queryParameters}";

        var httpRequest = new HttpRequestMessage(HttpMethod.Get, url);

        using var response = await _client.SendAsync(httpRequest, cancellationToken);
        if (!response.IsSuccessStatusCode)
        {
            return await HandleError(response, cancellationToken);
        }

        var members = await response.Content.ReadFromJsonAsync<MemberPagedListDto>(cancellationToken: cancellationToken);
        
        return members is null ? Result.Failure(DomainErrors.FailedToDeserializeObject) : Result.Success(members);
    }

    public async Task<Result> UpdateActivityAsync(Guid memberId, bool isActive,
        CancellationToken cancellationToken = default)
    {
        var url = $"{BaseUrl}/activity";

        var dto = new UpdateMemberActivityDto(memberId, isActive);

        var httpRequest = CreateHttpRequestMessage(dto, url, HttpMethod.Put);

        using var response = await _client.SendAsync(httpRequest, cancellationToken);
        if (response.IsSuccessStatusCode)
        {
            return Result.Success();
        }

        return await HandleError(response, cancellationToken);
    }

    public async Task<Result> RegisterAsync(RegisterMemberDto dto, CancellationToken cancellationToken = default)
    {
        var url = $"{BaseUrl}/registration";
        
        var httpRequest = CreateHttpRequestMessage(dto, url, HttpMethod.Post);

        using var response = await _client.SendAsync(httpRequest, cancellationToken);
        if (response.IsSuccessStatusCode)
        {
            return Result.Success();
        }

        return await HandleError(response, cancellationToken);
    }

    public async Task<Result> UpdateAsync(UpdateMemberDto dto, CancellationToken cancellationToken = default)
    {
        var httpRequest = CreateHttpRequestMessage(dto, BaseUrl, HttpMethod.Put);

        using var response = await _client.SendAsync(httpRequest, cancellationToken);
        if (response.IsSuccessStatusCode)
        {
            return Result.Success();
        }

        return await HandleError(response, cancellationToken);
    }

    private static HttpRequestMessage CreateHttpRequestMessage(object body, string url, HttpMethod httpMethod)
    {
        var httpRequest = new HttpRequestMessage(httpMethod, url);

        var json = JsonConvert.SerializeObject(body);
        httpRequest.Content = new StringContent(json, Encoding.UTF8, "application/json");

        return httpRequest;
    }

    private static async Task<Result> HandleError(HttpResponseMessage response, CancellationToken cancellationToken = default)
    {
        var error = await response.Content.ReadFromJsonAsync<Error>(cancellationToken: cancellationToken);

        return Result.Failure(error ?? DomainErrors.FailedToDeserializeObject);
    }
}
