using Microsoft.AspNetCore.Components.Authorization;
using MyToDo.Domain.Shared;
using MyToDo.HttpContracts.Members;
using MyToDo.Web.Extensions;
using MyToDo.Web.Services.Abstractions;

namespace MyToDo.Web.Services;

internal sealed class MemberService : BaseService, IMemberService
{
    public MemberService(IHttpClientFactory httpClientFactory, 
        AuthenticationStateProvider authenticationStateProvider) : base(httpClientFactory, authenticationStateProvider)
    {
    }

    protected override string BaseUrl => "members";

    public async Task<Result<List<MemberDto>>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        var httpRequest = await CreateHttpRequestMessage(HttpMethod.Get, BaseUrl);

        using var response = await HttpClient.SendAsync(httpRequest, cancellationToken);

        return await HandleResponse<List<MemberDto>>(response, cancellationToken);
    }

    public async Task<Result<MemberDto>> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var url = $"{BaseUrl}/{id}";

        var httpRequest = await CreateHttpRequestMessage(HttpMethod.Get, url);

        using var response = await HttpClient.SendAsync(httpRequest, cancellationToken);

        return await HandleResponse<MemberDto>(response, cancellationToken);
    }

    public async Task<Result<MemberPagedListDto>> GetPageAsync(MemberPageRequestDto dto, CancellationToken cancellationToken = default)
    {
        var queryParameters = await dto.GetQueryFromRequestDtoAsync();
        var url = $"{BaseUrl}/page?{queryParameters}";

        var httpRequest = await CreateHttpRequestMessage(HttpMethod.Get, url);

        using var response = await HttpClient.SendAsync(httpRequest, cancellationToken);

        return await HandleResponse<MemberPagedListDto>(response, cancellationToken);
    }

    public async Task<Result> UpdateActivityAsync(Guid memberId, bool isActive,
        CancellationToken cancellationToken = default)
    {
        var url = $"{BaseUrl}/activity";

        var dto = new UpdateMemberActivityDto
        {
            MemberId = memberId,
            IsActive = isActive
        };

        var httpRequest = await CreateHttpRequestMessage(HttpMethod.Put, url, dto);

        using var response = await HttpClient.SendAsync(httpRequest, cancellationToken);

        return await HandleResponse(response, cancellationToken);
    }

    public async Task<Result> RegisterAsync(RegisterMemberDto dto, CancellationToken cancellationToken = default)
    {
        var url = $"{BaseUrl}/registration";
        
        var httpRequest = await CreateHttpRequestMessage(HttpMethod.Post, url, dto);

        using var response = await HttpClient.SendAsync(httpRequest, cancellationToken);

        return await HandleResponse(response, cancellationToken);
    }

    public async Task<Result> UpdateAsync(UpdateMemberDto dto, CancellationToken cancellationToken = default)
    {
        var httpRequest = await CreateHttpRequestMessage(HttpMethod.Put, BaseUrl, dto);

        using var response = await HttpClient.SendAsync(httpRequest, cancellationToken);

        return await HandleResponse(response, cancellationToken);
    }

    public async Task<Result<MemberSessionDto>> LoginAsync(LoginDto dto, CancellationToken cancellationToken = default)
    {
        var url = $"{BaseUrl}/login";

        var httpRequest = await CreateHttpRequestMessage(HttpMethod.Post, url, dto);

        using var response = await HttpClient.SendAsync(httpRequest, cancellationToken);

        return await HandleResponse<MemberSessionDto>(response, cancellationToken);
    }

    public async Task<Result<MemberStatisticsDto>> GetMemberStatisticsAsync(Guid memberId, CancellationToken cancellationToken = default)
    {
        var url = $"{BaseUrl}/{memberId}/statistics";

        var httpRequest = await CreateHttpRequestMessage(HttpMethod.Get, url);

        using var response = await HttpClient.SendAsync(httpRequest, cancellationToken);

        return await HandleResponse<MemberStatisticsDto>(response, cancellationToken);
    }
}
