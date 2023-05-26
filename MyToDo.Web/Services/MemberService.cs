using MyToDo.Application.Common.Dtos.Members;
using MyToDo.Application.CQRS.Members.Commands.UpdateMemberActivityCommand;
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
        var query = await GetQueryFromDto(dto);
        var url = $"{_baseUrl}/page?{query}";
        
        var members = await _client.GetFromJsonAsync<MemberPagedListDto>(url, cancellationToken);

        return members;
    }

    public async Task UpdateActivityAsync(Guid memberId, bool isActive,
        CancellationToken cancellationToken = default)
    {
        var dto = new UpdateMemberActivityDto(memberId, isActive);

        await _client.PutAsJsonAsync($"{_baseUrl}/activity", dto, cancellationToken);
    }

    private async Task<string> GetQueryFromDto(MemberPageRequestDto dto)
    {
        var queryParameters = new Dictionary<string, string>();

        foreach (var property in dto.GetType().GetProperties())
        {
            if (property.GetValue(dto) is not null)
            {
                queryParameters.Add(property.Name, property.GetValue(dto).ToString());
            }
        }

        return await new FormUrlEncodedContent(queryParameters).ReadAsStringAsync();
    }
}
