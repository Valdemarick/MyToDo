using MyToDo.HttpContracts.Tags;
using MyToDo.Web.Extensions;
using MyToDo.Web.Services.Abstractions;

namespace MyToDo.Web.Services;

internal sealed class TagService : ITagService
{
    private readonly HttpClient _client;
    
    private const string BaseUrl = "tags";

    public TagService(IHttpClientFactory httpClientFactory)
    {
        _client = httpClientFactory.CreateClient("MyToDoServerClient");
    }
    
    public async Task<TagPagedListDto> GetPageAsync(TagPageRequestDto dto, CancellationToken cancellationToken = default)
    {
        var queryParameters = await dto.GetQueryFromRequestDto();
        var url = $"{BaseUrl}/page?{queryParameters}";
        
        var tags = await _client.GetFromJsonAsync<TagPagedListDto>(url, cancellationToken);

        return tags;
    }
    
    public async Task<List<TagDto>> GetAllTagsAsync(CancellationToken cancellationToken = default)
    {
        var allTags = await _client.GetFromJsonAsync<List<TagDto>>(BaseUrl, cancellationToken);
        
        return allTags ?? new List<TagDto>();
    }

    public async Task DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        await _client.DeleteAsync($"{BaseUrl}/{id}", cancellationToken);
    }
}