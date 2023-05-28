using MyToDo.Application.Common.Dtos.Common;
using MyToDo.Application.Common.Dtos.Members;
using MyToDo.Application.CQRS.Members.Commands.UpdateMemberActivityCommand;
using MyToDo.Web.Extensions;
using MyToDo.Web.Services.Abstractions;

namespace MyToDo.Web.Services;

internal sealed class MemberService : IMemberService
{
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly HttpClient _client;
    
    private const string _baseUrl = "members";

    public MemberService(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;

        _client = _httpClientFactory.CreateClient("MyToDoServerClient");
    }
    
    public async Task<MemberPagedListDto> GetPageAsync(MemberPageRequestDto dto, CancellationToken cancellationToken = default)
    {
        var queryParameters = await dto.GetQueryFromRequestDto();
        var url = $"{_baseUrl}/page?{queryParameters}";
        
        var members = await _client.GetFromJsonAsync<MemberPagedListDto>(url, cancellationToken);

        return members;
    }

    public async Task UpdateActivityAsync(Guid memberId, bool isActive,
        CancellationToken cancellationToken = default)
    {
        var dto = new UpdateMemberActivityDto(memberId, isActive);

        await _client.PutAsJsonAsync($"{_baseUrl}/activity", dto, cancellationToken);
    }
}
