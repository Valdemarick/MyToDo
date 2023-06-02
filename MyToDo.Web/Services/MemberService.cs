using System.Text;
using MyToDo.Domain.Errors;
using MyToDo.Domain.Shared;
using MyToDo.HttpContracts.Members;
using MyToDo.Web.Extensions;
using MyToDo.Web.Services.Abstractions;

namespace MyToDo.Web.Services;

internal sealed class MemberService : BaseService, IMemberService
{
    private readonly HttpClient _httpClient;
    
    public MemberService(IHttpClientFactory httpClientFactory)
    {
        _httpClient = httpClientFactory.CreateClient("MyToDoServerClient");
    }

    protected override string BaseUrl => "members";

    public async Task<Result<List<MemberDto>>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        var httpRequest = CreateHttpRequestMessage(HttpMethod.Get, BaseUrl);

        using var response = await _httpClient.SendAsync(httpRequest, cancellationToken);

        return await HandleResponse<List<MemberDto>>(response, cancellationToken);
    }

    public async Task<Result<MemberDto>> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var url = $"{BaseUrl}/{id}";

        var httpRequest = new HttpRequestMessage(HttpMethod.Get, url);

        using var response = await _httpClient.SendAsync(httpRequest, cancellationToken);

        return await HandleResponse<MemberDto>(response, cancellationToken);
    }

    public async Task<Result<MemberPagedListDto>> GetPageAsync(MemberPageRequestDto dto, CancellationToken cancellationToken = default)
    {
        var queryParameters = await dto.GetQueryFromRequestDto();
        var url = $"{BaseUrl}/page?{queryParameters}";

        var httpRequest = new HttpRequestMessage(HttpMethod.Get, url);

        using var response = await _httpClient.SendAsync(httpRequest, cancellationToken);

        return await HandleResponse<MemberPagedListDto>(response, cancellationToken);
    }

    public async Task<Result> UpdateActivityAsync(Guid memberId, bool isActive,
        CancellationToken cancellationToken = default)
    {
        var url = $"{BaseUrl}/activity";

        var dto = new UpdateMemberActivityDto(memberId, isActive);

        var httpRequest = CreateHttpRequestMessage(HttpMethod.Put, url, dto);

        using var response = await _httpClient.SendAsync(httpRequest, cancellationToken);

        return await HandleResponse(response, cancellationToken);
    }

    public async Task<Result> RegisterAsync(RegisterMemberDto dto, CancellationToken cancellationToken = default)
    {
        var url = $"{BaseUrl}/registration";
        
        var httpRequest = CreateHttpRequestMessage(HttpMethod.Post, url, dto);

        using var response = await _httpClient.SendAsync(httpRequest, cancellationToken);

        return await HandleResponse(response, cancellationToken);
    }

    public async Task<Result> UpdateAsync(UpdateMemberDto dto, CancellationToken cancellationToken = default)
    {
        var httpRequest = CreateHttpRequestMessage(HttpMethod.Put, BaseUrl, dto);

        using var response = await _httpClient.SendAsync(httpRequest, cancellationToken);

        return await HandleResponse(response, cancellationToken);
    }
}
