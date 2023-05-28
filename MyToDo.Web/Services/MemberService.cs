using MyToDo.HttpContracts.Members;
using MyToDo.Web.Extensions;
using MyToDo.Web.Services.Abstractions;

namespace MyToDo.Web.Services;

internal sealed class MemberService : IMemberService
{
    private readonly HttpClient _client;
    
    private const string BaseUrl = "members";

    public MemberService(IHttpClientFactory httpClientFactory)
    {
        _client = httpClientFactory.CreateClient("MyToDoServerClient");
    }
    
    public async Task<MemberPagedListDto> GetPageAsync(MemberPageRequestDto dto, CancellationToken cancellationToken = default)
    {
        var queryParameters = await dto.GetQueryFromRequestDto();
        var url = $"{BaseUrl}/page?{queryParameters}";
        
        var members = await _client.GetFromJsonAsync<MemberPagedListDto>(url, cancellationToken);

        return members;
    }

    public async Task UpdateActivityAsync(Guid memberId, bool isActive,
        CancellationToken cancellationToken = default)
    {
        var dto = new UpdateMemberActivityDto(memberId, isActive);

        await _client.PutAsJsonAsync($"{BaseUrl}/activity", dto, cancellationToken);
    }
}
